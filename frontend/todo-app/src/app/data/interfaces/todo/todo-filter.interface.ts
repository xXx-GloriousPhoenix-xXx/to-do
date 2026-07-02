export interface TodoFilter {
    category?: string | null;
    isCompleted?: boolean | null;
    completeUntilFrom?: string | null;
    completeUntilTo?: string | null;
}
