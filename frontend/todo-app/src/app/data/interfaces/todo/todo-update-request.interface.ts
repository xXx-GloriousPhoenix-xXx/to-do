export interface TodoUpdateRequest {
    title?: string | null;
    description?: string | null;
    category?: string | null;
    completeUntil?: string | null;
    isCompleted?: boolean | null;
}
