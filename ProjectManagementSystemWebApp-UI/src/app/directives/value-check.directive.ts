import { Directive, ElementRef } from '@angular/core';

@Directive({
  selector: '[appValueCheck]',
  standalone: false,
})
export class ValueCheckDirective {
  constructor(private readonly _element: ElementRef) {
    const elementContent = this._element.nativeElement.innerText;
    if (!elementContent) {
      this._element.nativeElement.innerText = '*NA*';
      this._element.nativeElement.style.color = 'red';
    }
  }
}
