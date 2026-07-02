import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'todoDate',
})
export class TodoDatePipe implements PipeTransform {
    transform(value: string | Date | null | undefined): string {
        if (!value) return '';

        const date = new Date(value);

        if (isNaN(date.getTime())) return '';

        const datePart = date.toLocaleDateString('en-GB', {
            day: '2-digit',
            month: 'short',
            year: 'numeric',
        });

        const timePart = date.toLocaleTimeString('en-GB', {
            hour: '2-digit',
            minute: '2-digit',
        });

        return `${datePart}, ${timePart}`;
    }
}
