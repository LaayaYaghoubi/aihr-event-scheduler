import { Routes } from '@angular/router';
import { EventsComponent } from './features/scheduled-event-list/components/events/events.component';
import { WelcomeComponent } from './shared/components/welcome/welcome.component';
import { DashboardComponent } from './shared/components/dashboard/dashboard.component';
import { CreateEditEventComponent } from './features/create-edit-event/create-edit-event.component';
import { LoginComponent } from './features/login/components/login.component';

export const routes: Routes = [
    { path: '', redirectTo: 'login', pathMatch: 'full' },
    { path: 'login', component: LoginComponent },
    { path: 'dashboard', component: DashboardComponent, children: [
        {path: '', component: WelcomeComponent},
        {path:'events', component: EventsComponent},
        {path:'createEvent', component: CreateEditEventComponent},
        {path:'editEvent/:id', component: CreateEditEventComponent},
       
    ]},
    { path: '**', redirectTo: 'login', pathMatch: 'full' }
];
