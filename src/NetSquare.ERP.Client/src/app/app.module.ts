import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AppHeaderComponent } from './shared/layouts/app-header/app-header.component';
import { ProjectDashboardComponent } from './project-dashboard/project-dashboard.component';
import { AppSidebarComponent } from './shared/layouts/app-sidebar/app-sidebar.component';
import { HrDashboardComponent } from './hr-dashboard/hr-dashboard.component';
import { ProjectsComponent } from './projects/projects/projects.component';
import { TasksComponent } from './projects/tasks/tasks.component';
import { TimesheetsComponent } from './projects/timesheets/timesheets.component';
import { LeadersComponent } from './projects/leaders/leaders.component';
import { TicketsComponent } from './tickets/tickets/tickets.component';
import { TicketDetailComponent } from './tickets/ticket-detail/ticket-detail.component';
import { ClientsComponent } from './our-clients/clients/clients.component';
import { ClientProfileComponent } from './our-clients/client-profile/client-profile.component';
import { MembersComponent } from './employees/members/members.component';
import { MemberProfileComponent } from './employees/member-profile/member-profile.component';
import { HolidaysComponent } from './employees/holidays/holidays.component';
import { AttendanceEmployeesComponent } from './employees/attendance-employees/attendance-employees.component';
import { AttendanceComponent } from './employees/attendance/attendance.component';
import { LeaveRequestComponent } from './employees/leave-request/leave-request.component';
import { DepartmentComponent } from './employees/department/department.component';
import { LoanComponent } from './employees/loan/loan.component';
import { InvoicesComponent } from './Accounts/invoices/invoices.component';
import { PaymentsComponent } from './Accounts/payments/payments.component';
import { ExpensesComponent } from './Accounts/expenses/expenses.component';
import { CreateInvoiceComponent } from './Accounts/create-invoice/create-invoice.component';
import { EmployeeSalaryComponent } from './Accounts/payroll/employee-salary/employee-salary.component';
import { CalanderComponent } from './App/calander/calander.component';
import { ChatComponent } from './App/chat/chat.component';
import { CalendarCommonModule } from 'angular-calendar';
import { ApexChartsComponent } from './Other-Pages/apex-charts/apex-charts.component';
import { FormsComponent } from './Other-Pages/forms/forms.component';
import { TableComponent } from './Other-Pages/table/table.component';
import { ReviewComponent } from './Other-Pages/review/review.component';
import { IconComponent } from './Other-Pages/icon/icon.component';
import { ContactComponent } from './Other-Pages/contact/contact.component';
import { WidgetsComponent } from './Other-Pages/widgets/widgets.component';
import { TodoListComponent } from './Other-Pages/todo-list/todo-list.component';
import { HElpComponent } from './help/h-elp/h-elp.component';
import { ChangeLogComponent } from './Change-log/change-log/change-log.component';
import { StaterPageComponent } from './Starter-page/stater-page/stater-page.component';
import { SignInComponent } from './Authentication/sign-in/sign-in.component';
import { SignUpComponent } from './Authentication/sign-up/sign-up.component';
import { ErrorComponent } from './Authentication/error/error.component';
import { PasswordResetComponent } from './Authentication/password-reset/password-reset.component';
import { TwoStepAuthComponent } from './Authentication/two-step-auth/two-step-auth.component';
import { ResignationComponent } from './Resignation/resignation/resignation.component';
import { FinalStatementComponent } from './Resignation/final-statement/final-statement.component';



@NgModule({
  declarations: [
    AppComponent,
    AppHeaderComponent,
    ProjectDashboardComponent,
    AppSidebarComponent,
    HrDashboardComponent,
    ProjectsComponent,
    TasksComponent,
    TimesheetsComponent,
    LeadersComponent,
    TicketsComponent,
    TicketDetailComponent,
    ClientsComponent,
    ClientProfileComponent,
    MembersComponent,
    MemberProfileComponent,
    HolidaysComponent,
    AttendanceEmployeesComponent,
    AttendanceComponent,
    LeaveRequestComponent,
    DepartmentComponent,
    LoanComponent,
    InvoicesComponent,
    PaymentsComponent,
    ExpensesComponent,
    CreateInvoiceComponent,
    EmployeeSalaryComponent,
    CalanderComponent,
    ChatComponent,
    ApexChartsComponent,
    FormsComponent,
    TableComponent,
    ReviewComponent,
    IconComponent,
    ContactComponent,
    WidgetsComponent,
    TodoListComponent,
    HElpComponent,
    ChangeLogComponent,
    StaterPageComponent,
    SignInComponent,
    SignUpComponent,
    PasswordResetComponent,
    ErrorComponent,
    TwoStepAuthComponent,
    ResignationComponent,
    FinalStatementComponent
    
    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    CalendarCommonModule
    
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
