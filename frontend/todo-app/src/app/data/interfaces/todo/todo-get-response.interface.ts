export interface TodoGetResponse {
    id: string;
    authorId: string;
    authorUsername: string;
    title: string;
    description: string;
    category: string;
    completeUntil: string;
    isCompleted: boolean;
}
