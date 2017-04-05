import { Pipe, PipeTransform, Injectable } from "@angular/core";

@Pipe({
    name: 'filterList',
    pure: false
})
@Injectable()
export class FilterPipe implements PipeTransform {
    transform(items: any[], term: string, properties: string[]): any {
        if (term === undefined || "") {
            return items;
        }

        return items.filter(item => {
            return Object.keys(item).some(k => {
                if (properties) {
                    if (properties.indexOf(k) !== -1) {
                        return this.checkItem(item, term, k);
                    } else {
                        return false;
                    }
                } else {
                    return this.checkItem(item, term, k);
                }
            });
        });
    }

    checkItem(item: any, term: string, property: string) {
        if (item[property] == null) {
            return false;
        } else if (typeof item[property] === "string") {
            return item[property].toLowerCase().indexOf(term.toLowerCase()) !== -1;
        }
        else {
            return item[property] == term.toLowerCase();
        }
    }
}