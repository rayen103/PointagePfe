import { Injectable } from '@angular/core';
import { FuseMockApiService } from '@fuse/lib/mock-api';
import {
    chats as chatsData,
    contacts as contactsData,
    messages as messagesData,
    profile as profileData,
} from 'app/mock-api/apps/chat/data';
import { assign, cloneDeep, omit } from 'lodash-es';

@Injectable({ providedIn: 'root' })
export class ChatMockApi {
    private _chats: any[] = chatsData;
    private _contacts: any[] = contactsData;
    private _messages: any[] = messagesData;
    private _profile: any = profileData;

    /**
     * Constructor
     */
    constructor(private _fuseMockApiService: FuseMockApiService) {
        // Register Mock API handlers
        this.registerHandlers();

        // Modify the chats array to attach certain data to it
        this._chats = this._chats.map((chat) => ({
            ...chat,
            // Get the actual contact object from the id and attach it to the chat
            contact: this._contacts.find(
                (contact) => contact.id === chat.contactId
            ),
            // Since we use same set of messages on all chats, we assign them here.
            messages: chat.id === 'gemini-chat-id' ? [
                {
                    id: 'gemini-msg-1',
                    chatId: 'gemini-chat-id',
                    contactId: 'gemini-contact-id',
                    isMine: false,
                    value: 'Bonjour Brian ! Je suis votre assistant Gemini spécialisé dans ce projet de gestion de collecte et de pointage. Je connais parfaitement la structure de votre backend .NET, de votre frontend Angular et de vos services de Machine Learning.',
                    createdAt: new Date().toISOString()
                },
                {
                    id: 'gemini-msg-2',
                    chatId: 'gemini-chat-id',
                    contactId: 'gemini-contact-id',
                    isMine: false,
                    value: 'Je peux vous aider sur les points suivants :\n- Explication de l\'architecture (CQRS, MediatR, Carter)\n- Détails des modules frontend (CollectManagement, CST)\n- Fonctionnement des modèles ML (STGCN, RL Dispatcher, Predictive Maintenance)\n- Gestion des utilisateurs et des rôles\n- Et bien plus encore !',
                    createdAt: new Date().toISOString()
                },
                {
                    id: 'gemini-msg-3',
                    chatId: 'gemini-chat-id',
                    contactId: 'gemini-contact-id',
                    isMine: false,
                    value: 'Quelle est votre question sur le projet ?',
                    createdAt: new Date().toISOString()
                }
            ] : this._messages.map((message) => ({
                ...message,
                chatId: chat.id,
                contactId:
                    message.contactId === 'me'
                        ? this._profile.id
                        : chat.contactId,
                isMine: message.contactId === 'me',
            })),
        }));
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Public methods
    // -----------------------------------------------------------------------------------------------------

    /**
     * Register Mock API handlers
     */
    registerHandlers(): void {
        // -----------------------------------------------------------------------------------------------------
        // @ Chats - GET
        // -----------------------------------------------------------------------------------------------------
        this._fuseMockApiService.onGet('api/apps/chat/chats').reply(() => {
            // Clone the chats
            const chats = cloneDeep(this._chats);

            // Return the response
            return [200, chats];
        });

        // -----------------------------------------------------------------------------------------------------
        // @ Chat - GET
        // -----------------------------------------------------------------------------------------------------
        this._fuseMockApiService
            .onGet('api/apps/chat/chat')
            .reply(({ request }) => {
                // Get the chat id
                const id = request.params.get('id');

                // Clone the chats
                const chats = cloneDeep(this._chats);

                // Find the chat we need
                const chat = chats.find((item) => item.id === id);

                // Return the response
                return [200, chat];
            });

        // -----------------------------------------------------------------------------------------------------
        // @ Chat - PATCH
        // -----------------------------------------------------------------------------------------------------
        this._fuseMockApiService
            .onPatch('api/apps/chat/chat')
            .reply(({ request }) => {
                // Get the id and chat
                const id = request.body.id;
                const chat = cloneDeep(request.body.chat);

                // Prepare the updated chat
                let updatedChat = null;

                // Find the chat and update it
                this._chats.forEach((item, index, chats) => {
                    if (item.id === id) {
                        // Update the chat
                        chats[index] = assign({}, chats[index], chat);

                        // Store the updated chat
                        updatedChat = chats[index];
                    }
                });

                // Return the response
                return [200, updatedChat];
            });

        // -----------------------------------------------------------------------------------------------------
        // @ Contacts - GET
        // -----------------------------------------------------------------------------------------------------
        this._fuseMockApiService.onGet('api/apps/chat/contacts').reply(() => {
            // Clone the contacts
            let contacts = cloneDeep(this._contacts);

            // Sort the contacts by the name field by default
            contacts.sort((a, b) => a.name.localeCompare(b.name));

            // Omit details and attachments from contacts
            contacts = contacts.map((contact) =>
                omit(contact, ['details', 'attachments'])
            );

            // Return the response
            return [200, contacts];
        });

        // -----------------------------------------------------------------------------------------------------
        // @ Contact Details - GET
        // -----------------------------------------------------------------------------------------------------
        this._fuseMockApiService
            .onGet('api/apps/chat/contact')
            .reply(({ request }) => {
                // Get the contact id
                const id = request.params.get('id');

                // Clone the contacts
                const contacts = cloneDeep(this._contacts);

                // Find the contact
                const contact = contacts.find((item) => item.id === id);

                // Return the response
                return [200, contact];
            });

        // -----------------------------------------------------------------------------------------------------
        // @ Profile - GET
        // -----------------------------------------------------------------------------------------------------
        this._fuseMockApiService.onGet('api/apps/chat/profile').reply(() => {
            // Clone the profile
            const profile = cloneDeep(this._profile);

            // Return the response
            return [200, profile];
        });

        // -----------------------------------------------------------------------------------------------------
        // @ Message - POST
        // -----------------------------------------------------------------------------------------------------
        this._fuseMockApiService
            .onPost('api/apps/chat/message')
            .reply(({ request }) => {
                // Get the chat id and message
                const chatId = request.body.chatId;
                const message = cloneDeep(request.body.message);

                // Add the message to the chat
                this._chats.forEach((item) => {
                    if (item.id === chatId) {
                        // Add the message
                        item.messages.push({
                            ...message,
                            id: Math.random().toString(36).substring(2, 15),
                            chatId,
                            contactId: this._profile.id,
                            isMine: true,
                        });
                    }
                });

                // Return the response
                return [
                    200,
                    {
                        message: {
                            ...message,
                            id: Math.random().toString(36).substring(2, 15),
                            chatId,
                            contactId: this._profile.id,
                            isMine: true,
                        },
                    },
                ];
            });

        // -----------------------------------------------------------------------------------------------------
        // @ Gemini Response - POST
        // -----------------------------------------------------------------------------------------------------
        this._fuseMockApiService
            .onPost('api/apps/chat/gemini-response')
            .reply(({ request }) => {
                const chatId = request.body.chatId;
                const userMessage = request.body.userMessage.toLowerCase();

                let aiValue = '';

                // Intelligent responses based on project context
                if (userMessage.includes('architecture') || userMessage.includes('structure')) {
                    aiValue = 'L\'architecture du projet est basée sur le **Clean Architecture** côté backend (.NET 8) avec CQRS (MediatR) et Carter pour les endpoints. Côté frontend, nous utilisons **Angular 18** avec une structure modulaire par domaine (CollectManagement, CST).';
                } else if (userMessage.includes('ml') || userMessage.includes('ia') || userMessage.includes('machine learning')) {
                    aiValue = 'Le projet intègre plusieurs services ML :\n- **STGCN** pour la prédiction du trafic.\n- **RL Dispatcher** pour l\'optimisation des tournées.\n- **Scoring d\'absence** via des modèles de classification.\n- **Maintenance prédictive** pour les bus.';
                } else if (userMessage.includes('collect') || userMessage.includes('circuit')) {
                    aiValue = 'Le module **CollectManagement** gère les circuits de collecte, le pointage des employés et les ordres de travail (OT). Les circuits sont visualisables sur une carte interactive via Leaflet.';
                } else if (userMessage.includes('hello') || userMessage.includes('bonjour') || userMessage.includes('salut')) {
                    aiValue = 'Bonjour ! Je suis prêt à vous guider à travers l\'application. Que voulez-vous savoir sur le système de pointage, les circuits ou les modèles d\'IA ?';
                } else {
                    aiValue = 'C\'est une excellente question sur le projet PointagePfe. Pourriez-vous préciser si vous parlez du backend .NET, du frontend Angular ou d\'un service ML spécifique ? Je peux vous expliquer comment chaque partie communique via le Bus de données.';
                }

                const aiMessage = {
                    id: Math.random().toString(36).substring(2, 15),
                    chatId,
                    contactId: 'gemini-contact-id',
                    isMine: false,
                    value: aiValue,
                    createdAt: new Date().toISOString(),
                };

                // Add the message to the chat
                this._chats.forEach((item) => {
                    if (item.id === chatId) {
                        item.messages.push(aiMessage);
                    }
                });

                return [200, { message: aiMessage }];
            });
    }
}
