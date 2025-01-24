import { Routes } from '@angular/router';
import { DashboardComponent } from '../presentation/components/dashboard/dashboard.component';
import { EventsComponent } from '../presentation/components/events/events.component';
import { WelcomeComponent } from '../presentation/components/welcome/welcome.component';
import { CreateEditEventComponent } from '../presentation/components/create-edit-event/create-edit-event.component';

export const routes: Routes = [
    {path: '', redirectTo: 'dashboard', pathMatch: 'full'},
    {path: 'dashboard', component: DashboardComponent, children: [
        {path: '', component: WelcomeComponent},
        {path:'events', component: EventsComponent},
        {path:'createEvent', component: CreateEditEventComponent},
        {path:'editEvent/:id', component: CreateEditEventComponent}
    ]},
    {path: '**', redirectTo: 'dashboard', pathMatch: 'full'},
];
