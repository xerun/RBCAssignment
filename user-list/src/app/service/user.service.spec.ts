import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { UserService } from './user.service';
import { UserDto } from '../models';

describe('UserService', () => {
    let service: UserService;
    let httpTestingController: HttpTestingController;
  
    beforeEach(() => {
      TestBed.configureTestingModule({
        imports: [ HttpClientTestingModule ],
        providers: [ UserService ]
      });
      service = TestBed.inject(UserService);
      httpTestingController = TestBed.inject(HttpTestingController);
    });
  
    afterEach(() => {
      httpTestingController.verify();
    });
  
    it('should be created', () => {
      expect(service).toBeTruthy();
    });
  
    it('should return a list of users', () => {
      const mockUsers: UserDto[] = [
        {
            "id": 9,
            "name": "Glenna Reichert",
            "username": "Delphine",
            "email": "Chaim_McDermott@dana.io",
            "address": {
              "street": "Dayna Park",
              "suite": "Suite 449",
              "city": "Bartholomebury",
              "zipcode": "76495-3109",
              "geo": {
                "lat": 24.6463,
                "lng": -168.8889
              }
            },
            "phone": "(775)976-6794 x41206",
            "website": "conrad.com",
            "company": {
              "name": "Yost and Sons",
              "catchPhrase": "Switchable contextually-based project",
              "bs": "aggregate real-time technologies"
            }
        },
        {
            "id": 10,
            "name": "Clementina DuBuque",
            "username": "Moriah.Stanton",
            "email": "Rey.Padberg@karina.biz",
            "address": {
              "street": "Kattie Turnpike",
              "suite": "Suite 198",
              "city": "Lebsackbury",
              "zipcode": "31428-2261",
              "geo": {
                "lat": -38.2386,
                "lng": 57.2232
              }
            },
            "phone": "024-648-3804",
            "website": "ambrose.net",
            "company": {
              "name": "Hoeger LLC",
              "catchPhrase": "Centralized empowering task-force",
              "bs": "target end-to-end models"
            }
        }
      ];
  
      service.getUsers().subscribe(users => {
        expect(users.length).toBeGreaterThan(0);
        expect(users).toEqual(mockUsers);
      });
  
      // check if a single request is made to the url
      const req = httpTestingController.expectOne('https://jsonplaceholder.typicode.com/users');
      // check if the method is GET
      expect(req.request.method).toEqual('GET');
  
      req.flush(mockUsers);
    });
  });