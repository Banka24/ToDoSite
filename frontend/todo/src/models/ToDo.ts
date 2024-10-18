export interface IToDo {
    id: number;
    title: string;
    description: string;
    lastDay: Date | string;
    isActive: boolean;
}