import { FormControl } from "@angular/forms";

export interface SignUpForm {
    username: FormControl<string>,
    email: FormControl<string>,
    password: FormControl<string>
}
