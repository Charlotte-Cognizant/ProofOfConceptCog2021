import { template } from "@angular-devkit/schematics";
import { Component } from "@angular/core";
//angular.ioguide/user-input
@Component({
selector: 'app-key-up2',
template:`
    
    <input #box (keyup) = "onKey(box.value)">
    <p>{{values}}</p>
    
`
})
export class KeyUpComponent{
    values = ' ';
    onKey(value:string) {
        this.values += value +' | ';
    }

}

   