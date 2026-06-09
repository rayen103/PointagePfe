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
                    value: 'Bonjour Brian ! Je suis votre assistant Gemini spécialisé dans les données de votre application de gestion de collecte et de pointage. Je connais toutes les entités et leurs champs !',
                    createdAt: new Date().toISOString()
                },
                {
                    id: 'gemini-msg-2',
                    chatId: 'gemini-chat-id',
                    contactId: 'gemini-contact-id',
                    isMine: false,
                    value: 'Je peux vous aider sur :\n- Les entités et leurs champs (Bus, Employé, Circuit, etc.)\n- Les relations entre les données\n- Les informations stockées pour chaque enregistrement\n- Et bien plus encore !',
                    createdAt: new Date().toISOString()
                },
                {
                    id: 'gemini-msg-3',
                    chatId: 'gemini-chat-id',
                    contactId: 'gemini-contact-id',
                    isMine: false,
                    value: 'Par exemple, demandez-moi : "Quels champs a un Bus ?" ou "Quelles informations sont stockées pour un Employé ?"',
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

                // Entity-specific responses
                if (userMessage.includes('bus') && (userMessage.includes('champs') || userMessage.includes('champ') || userMessage.includes('informations') || userMessage.includes('données') || userMessage.includes('fields') || userMessage.includes('information') || userMessage.includes('data'))) {
                    aiValue = '**Entité Bus** : Voici tous les champs stockés pour un bus :\n' +
                        '- **BusId** : Identifiant unique du bus\n' +
                        '- **NumeroIMM** : Numéro d\'immatriculation (obligatoire)\n' +
                        '- **ModelBus** : Modèle du bus\n' +
                        '- **IMEI** : Numéro IMEI du dispositif\n' +
                        '- **Capacite** : Capacité maximale du bus\n' +
                        '- **CodeCircuit** : Code du circuit associé\n' +
                        '- **CodeChauffeur** : Code du chauffeur associé\n' +
                        '- **AppSagem** : Booléen, si l\'app Sagem est utilisée\n' +
                        '- **IsActive** : Booléen, si le bus est actif\n' +
                        '- **Latitude / Longitude** : Position GPS du bus\n' +
                        '- **CurrentOccupancy** : Nombre de passagers actuels\n' +
                        '- **LastPositionAt** : Date de la dernière mise à jour de position\n' +
                        '- **LastOccupancyUpdateAt** : Date de la dernière mise à jour d\'occupation\n' +
                        '- **SocieteId** : Identifiant de la société propriétaire';
                } else if (userMessage.includes('employé') || userMessage.includes('employe') && (userMessage.includes('champs') || userMessage.includes('champ') || userMessage.includes('informations') || userMessage.includes('données') || userMessage.includes('fields') || userMessage.includes('information') || userMessage.includes('data'))) {
                    aiValue = '**Entité Employé** : Voici tous les champs stockés pour un employé :\n' +
                        '- **EmployeId** : Identifiant unique\n' +
                        '- **Matricule** : Matricule de l\'employé (obligatoire)\n' +
                        '- **RFID** : Tag RFID pour pointage\n' +
                        '- **Nom / Prenom** : Nom et prénom\n' +
                        '- **TypeEmploye** : Type d\'employé (énumération)\n' +
                        '- **CodeCircuit** : Circuit affecté\n' +
                        '- **CodePointCollecte** : Point de collecte affecté\n' +
                        '- **CodeBus** : Bus affecté\n' +
                        '- **CodeShift** : Shift/horaire affecté\n' +
                        '- **Adresse** : Adresse\n' +
                        '- **CodeGouvernorat / CodeRegion** : Localisation\n' +
                        '- **Latitude / Longitude** : Position GPS\n' +
                        '- **SocieteId** : Société d\'affectation';
                } else if (userMessage.includes('circuit') && (userMessage.includes('champs') || userMessage.includes('champ') || userMessage.includes('informations') || userMessage.includes('données') || userMessage.includes('fields') || userMessage.includes('information') || userMessage.includes('data'))) {
                    aiValue = '**Entité Circuit** : Voici tous les champs stockés pour un circuit :\n' +
                        '- **CircuitId** : Identifiant unique\n' +
                        '- **CodeCircuit** : Code unique du circuit (obligatoire)\n' +
                        '- **LibelleCircuit** : Libellé/nom du circuit\n' +
                        '- **Description** : Description du circuit\n' +
                        '- **IsActive** : Booléen (actif/inactif)\n' +
                        '- **Latitude / Longitude** : Position GPS de référence\n' +
                        '- **CodePCDepart** : Point de collecte de départ\n' +
                        '- **CodePCArrivee** : Point de collecte d\'arrivée\n' +
                        '- **DistanceKm** : Distance du circuit en kilomètres\n' +
                        '- **DureeMinutes** : Durée estimée du circuit\n' +
                        '- **Couleur** : Couleur pour l\'affichage\n' +
                        '- **SocieteId** : Société propriétaire';
                } else if (userMessage.includes('entité') || userMessage.includes('entity') || userMessage.includes('entites') || userMessage.includes('entities')) {
                    aiValue = 'Voici la liste de toutes les entités principales de l\'application :\n' +
                        '- **Bus** : Véhicules de collecte\n' +
                        '- **Employé** : Personnel (chauffeurs, agents, etc.)\n' +
                        '- **Circuit** : Itinéraires de collecte\n' +
                        '- **PointCollecte** : Points où les bus s\'arrêtent\n' +
                        '- **Chantier** : Chantiers de travail\n' +
                        '- **Equipe** : Équipes de travail\n' +
                        '- **Rattachement** : Affectations\n' +
                        '- **OrdreTravail** : Ordres de travail (OT)\n' +
                        '- **Utilisateur** : Comptes utilisateurs de l\'application\n' +
                        '- **Societe** : Sociétés clientes\n' +
                        'Demandez-moi les détails sur une entité spécifique !';
                } else if (userMessage.includes('hello') || userMessage.includes('bonjour') || userMessage.includes('salut')) {
                    aiValue = 'Bonjour ! Je suis prêt à vous aider avec les données de votre application. Posez-moi des questions sur les entités (Bus, Employé, Circuit, etc.) !';
                } else {
                    aiValue = 'Je suis ici pour vous aider avec vos données ! Essayez de demander :\n' +
                        '"Quels champs a un Bus ?"\n' +
                        '"Quelles informations pour un Employé ?"\n' +
                        '"Quelles sont les entités de l\'application ?"';
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
