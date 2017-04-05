import { Pipe, PipeTransform, Injectable } from "@angular/core";

@Pipe({
    name: 'replace',
    pure: false
})
@Injectable()
export class ReplacePipe implements PipeTransform {
    transform(item: string, searchValue: string, replaceValue: string): any {
        var regex = new RegExp(searchValue, "igm");
        return item.replace(regex, replaceValue);
    }
}