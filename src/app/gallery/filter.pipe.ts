import { Pipe, PipeTransform } from '@angular/core';
//Sources:https://mdbootstrap.com/docs/angular/forms/search/
//Charlotte Roux 
@Pipe({
  name: 'filter'
})
export class FilterPipe implements PipeTransform {
  transform(items: any[], searchText: string): any[] {

    if (!items) {
      return [];
    }
    if (!searchText) {
      return items;
    }
    searchText = searchText.toLowerCase();
    return items.filter(it => {
      return it.address.toLowerCase().includes(searchText);
    });
  }
}
// 42-69-72-64