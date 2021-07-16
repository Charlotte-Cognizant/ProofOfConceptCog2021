import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class MapImagesService {
  imagePath:any;

  constructor(private http:HttpClient) {   }

  getImagepaths(){
    return this.http.get("https://localhost:5001/api/addressPage/imageList")
  }
}
