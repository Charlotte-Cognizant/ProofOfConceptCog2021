import {Component, OnInit} from '@angular/core'

@Component ({templateUrl:'addressPage.component.html'})

@Component(

    {selector: 'addressPage-route',
    templateUrl: './addressPage.component.html',
    styleUrls: ['./addressPage.component.css']
})

export class addressComponent implements OnInit{
    private mapQuestKey = 'FCG8OAH64qxVqVG1MQRqWnDiBY0kwkzA';
    style = 'mapbox://styles/mapbox/streets-v11';
    lat = 37.75;
     lng = -122.41;
    ngOnInit(){

    }
    //gets specific map information and then loads it all up for the viewer.



}

//Going to want to get information from the database as a photo.


//containts detailed information about any individual data
