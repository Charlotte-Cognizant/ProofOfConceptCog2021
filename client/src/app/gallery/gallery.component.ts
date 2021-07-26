import {Component, EventEmitter, OnChanges, OnInit, Output, SimpleChange, SimpleChanges,ChangeDetectorRef, ChangeDetectionStrategy} from '@angular/core'
import { HttpClient } from '@angular/common/http';
import { FormControl, FormGroup } from '@angular/forms';
import { Console } from 'console';

@Component({templateUrl:'./gallery.component.html',
styleUrls: ['./gallery.component.css'],
changeDetection: ChangeDetectionStrategy.Default
}
)


export class GalleryComponent implements OnInit, OnChanges{
    address: any;
    indivaddress: any;
    addressPath: any;
    spatialList: any;
    textSearch!: string;
    searchAddressList:any;
    searchString!: string;
    @Output() private onFormChange = new EventEmitter<any>();
    searchFormGroup = new FormGroup({
        search:new FormControl(''),
    })
    searchValue = new FormControl('');


    
    constructor(private http:HttpClient,
        private changeDetection :ChangeDetectorRef
        
        ){}
    //adding search bar documentation
    //https://mdbootstrap.com/docs/angular/forms/search/
    ngOnInit(){
        const formValue=this.searchFormGroup.get("search");
        this.textSearch=formValue?.value;
        this.searchAddressList=this.getAllImages();
        //this.searchAddressList=this.Search();
        this.changeDetection.detectChanges();
    }
    ngOnChanges(){
      
        
    }

    //On changes subscribe to formControl https://www.digitalocean.com/community/tutorials/angular-reactive-forms-valuechanges
    //62697264a

    getAllImages(){
        this.http.get('https://localhost:5001/api/addressPage').subscribe(response =>{
            this.spatialList=response;
            this.spatialList.reverse();
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
