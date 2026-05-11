import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Chat } from 'app/layout/common/quick-chat/quick-chat.types';
import {
    BehaviorSubject,
    map,
    Observable,
    of,
    switchMap,
    tap,
    throwError,
} from 'rxjs';

@Injectable({ providedIn: 'root' })
export class QuickChatService {
    private _chat: BehaviorSubject<Chat> = new BehaviorSubject(null);
    private _chats: BehaviorSubject<Chat[]> = new BehaviorSubject<Chat[]>(null);
    private _opened: BehaviorSubject<boolean> = new BehaviorSubject(false);

    /**
     * Constructor
     */
    constructor(private _httpClient: HttpClient) {}

    // -----------------------------------------------------------------------------------------------------
    // @ Accessors
    // -----------------------------------------------------------------------------------------------------

    /**
     * Getter for chat
     */
    get chat$(): Observable<Chat> {
        return this._chat.asObservable();
    }

    /**
     * Getter for chats
     */
    get chats$(): Observable<Chat[]> {
        return this._chats.asObservable();
    }

    /**
     * Getter for opened
     */
    get opened$(): Observable<boolean> {
        return this._opened.asObservable();
    }

    /**
     * Setter for opened
     * @param value
     */
    setOpened(value: boolean): void {
        this._opened.next(value);
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Public methods
    // -----------------------------------------------------------------------------------------------------

    /**
     * Get chats
     */
    getChats(): Observable<any> {
        return this._httpClient.get<Chat[]>('api/apps/chat/chats').pipe(
            tap((response: Chat[]) => {
                this._chats.next(response);
            })
        );
    }

    /**
     * Get chat
     *
     * @param id
     */
    getChatById(id: string): Observable<any> {
        return this._httpClient
            .get<Chat>('api/apps/chat/chat', { params: { id } })
            .pipe(
                map((chat) => {
                    // Update the chat
                    this._chat.next(chat);

                    // Return the chat
                    return chat;
                }),
                switchMap((chat) => {
                    if (!chat) {
                        return throwError(
                            'Could not found chat with id of ' + id + '!'
                        );
                    }

                    return of(chat);
                })
            );
    }

    /**
     * Store message
     *
     * @param chatId
     * @param message
     */
    createMessage(chatId: string, message: any): Observable<any> {
        return this._httpClient
            .post<any>('api/apps/chat/message', {
                chatId,
                message,
            })
            .pipe(
                tap((response) => {
                    // Get the chats
                    const chats = this._chats.getValue();

                    // Find the chat index
                    const chatIndex = chats.findIndex((item) => item.id === chatId);

                    // Update the last message
                    chats[chatIndex].lastMessage = response.message.value;
                    chats[chatIndex].lastMessageAt = response.message.createdAt;

                    // Update the chats
                    this._chats.next(chats);

                    // Update the chat
                    const chat = this._chat.getValue();
                    if (chat && chat.id === chatId) {
                        chat.messages.push(response.message);
                        this._chat.next(chat);
                    }

                    // If it's the Gemini chat, trigger a mock AI response
                    if (chatId === 'gemini-chat-id') {
                        setTimeout(() => {
                            this._httpClient
                                .post<any>('api/apps/chat/gemini-response', {
                                    chatId,
                                    userMessage: message.value,
                                })
                                .subscribe((aiResponse) => {
                                    const currentChat = this._chat.getValue();
                                    if (currentChat && currentChat.id === chatId) {
                                        currentChat.messages.push(aiResponse.message);
                                        this._chat.next(currentChat);

                                        // Also update last message in list
                                        const currentChats = this._chats.getValue();
                                        const gIndex = currentChats.findIndex((item) => item.id === chatId);
                                        currentChats[gIndex].lastMessage = aiResponse.message.value;
                                        currentChats[gIndex].lastMessageAt = aiResponse.message.createdAt;
                                        this._chats.next(currentChats);
                                    }
                                });
                        }, 1000);
                    }
                })
            );
    }
}
