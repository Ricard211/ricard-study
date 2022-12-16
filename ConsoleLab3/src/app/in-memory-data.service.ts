import { Injectable } from '@angular/core';
import { InMemoryDbService } from 'angular-in-memory-web-api';
import { Console } from './console';

@Injectable({
  providedIn: 'root',
})
export class InMemoryDataService implements InMemoryDbService {
  createDb() {
    const consoles = [
      {
        id: 1,
        name: 'Sega Genesis',
        img: './images/sega',
      },
      {
        id: 2,
        name: 'Playstation 5',
        img: './images/ps5',
      },
      {
        id: 3,
        name: 'Xbox 360',
        img: './images/xbox360',
      },
      {
        id: 4,
        name: 'Playstation',
        img: './images/ps1',
      },
      {
        id: 5,
        name: 'Playstation 2',
        img: './images/ps2',
      },
      {
        id: 6,
        name: 'Playstation 3',
        img: './images/ps3',
      },
      {
        id: 7,
        name: 'Playstation 4',
        img: './images/ps4',
      },
      {
        id: 8,
        name: 'Xbox',
        img: './images/xboxo',
      },
      {
        id: 9,
        name: 'Xbox One',
        img: './images/xbox1',
      },
      {
        id: 10,
        name: 'Xbox Series X',
        img: './images/xboxsx',
      },
    ];
    return { consoles };
  }

  // Overrides the genId method to ensure that a hero always has an id.
  // If the consoles array is empty,
  // the method below returns the initial number (11).
  // if the consoles array is not empty, the method below returns the highest
  // hero id + 1.
  genId(consoles: Console[]): number {
    return consoles.length > 0
      ? Math.max(...consoles.map((console) => console.id)) + 1
      : 11;
  }
}
