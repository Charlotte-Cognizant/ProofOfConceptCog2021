import { HttpClient } from '@angular/common/http';
import { OnInit } from '@angular/core';
import { Component } from '@angular/core';

@Component({
    selector: 'app-loop-back',
    template:`
    <input #box (keyup) = "0">
    <p> {{box.value}} </p>
    `

})
export class LoopbackComponent{ }