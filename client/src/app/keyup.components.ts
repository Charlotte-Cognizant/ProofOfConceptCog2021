import { template } from "@angular-devkit/schematics";

template:`
    <input (keyup) = "onKey($event)">
    <p>{{values}}</p>
    angular.ioguide/user-input
`
export class KeyUpComponent_v1{
    values = ' ';
    onKey(event: KeyboardEvent) {
        this.values += (event.target as HTMLInputElement).value +' | ';
    }
}

   