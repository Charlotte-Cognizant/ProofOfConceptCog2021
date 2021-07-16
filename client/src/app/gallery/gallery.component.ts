import {Component, OnChanges, OnInit} from '@angular/core'
import { HttpClient } from '@angular/common/http';
@Component({templateUrl:'gallery.component.html'})
export class GalleryComponent implements OnInit, OnChanges{
    address: any;
    indivaddress: any;
    addressPath: any;
    spatialList: any;
    
    constructor(private http:HttpClient){}

    ngOnInit(){
        this.getAllImages();
    }
    ngOnChanges(){
        this.getAllImages();
    }




    getAllImages(){
        this.http.get('https://localhost:5001/api/addressPage').subscribe(response =>{
            this.spatialList=response;
            return this.spatialList;
        })
    }
    getAddress(id: { toString: () => string; }){
        this.http.get('https://localhost:5001/api/addressPage/' + id).subscribe(response=> {
            this.indivaddress=response;
            console.log(this.indivaddress);
            },(error:any)=>{
                console.log(error);
            })
            return this.indivaddress;
    }
    getImagePath(id: string){
        this.http.get('https://localhost:5001/api/addressPage/path/' + id).subscribe(response => {
            this.addressPath=response;
            console.error(this.addressPath.imagePath);
        })
        return (this.addressPath.imagePath)
    }        
    
    getAddresss(){
     //From our database we subscribe in order to ask for some data, we ask for users => is a Typescript function I dont understand super well.
     //Basically tho is says the response to our http request api/user as seen when running the backend http request is used in order to get the results. This.users = 
     //output from API call.
     this.http.get('https://localhost:5001/api/addressPage').subscribe(response => {
      this.address = response;
       console.log(this.address);
     
     },(error: any) => {
       console.log(error);
     })
   }


}
