import { Component, OnInit } from '@angular/core';
import { AuthService } from '../auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-auth-callback',
  templateUrl: './auth-callback.component.html',
  styleUrls: ['./auth-callback.component.css']
})
export class AuthCallbackComponent implements OnInit {
  constructor(private authService: AuthService, private router: Router) { }
  ngOnInit() {
    this.authService.authCallback().subscribe(_ => {
      this.authService.initAuth().then(() => {
        this.router.navigate(['/']);
      });
    }, _ => this.router.navigate(['/']));
  }
}
