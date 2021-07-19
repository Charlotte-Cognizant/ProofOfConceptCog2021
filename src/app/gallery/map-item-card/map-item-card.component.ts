import { isDelegatedFactoryMetadata } from '@angular/compiler/src/render3/r3_factory';
import { Component, Input, OnInit } from '@angular/core';
import { image } from 'src/app/_models/image';

@Component({
  selector: 'app-map-item-card',
  templateUrl: './map-item-card.component.html',
  styleUrls: ['./map-item-card.component.css']
})
export class MapItemCardComponent implements OnInit {
  @Input() imageData!: image;
  sourceString:any;
  document:any;
  infostring: any;

  constructor() { }

  ngOnInit()
  {
    this.infostring = this.imageData.address;
    this.sourceString = "data:image/png;base64," + this.imageData.imagebyte;
  }

}
