import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateInvoiceComponent } from './Accounts/create-invoice/create-invoice.component';
import { ExpensesComponent } from './Accounts/expenses/expenses.component';
import { InvoicesComponent } from './Accounts/invoices/invoices.component';
import { PaymentsComponent } from './Accounts/payments/payments.component';
import { EmployeeSalaryComponent } from './Accounts/payroll/employee-salary/employee-salary.component';
import { CalanderComponent } from './App/calander/calander.component';
import { ChatComponent } from './App/chat/chat.component';
import { ErrorComponent } from './Authentication/error/error.component';
import { PasswordResetComponent } from './Authentication/password-reset/password-reset.component';
import { SignInComponent } from './Authentication/sign-in/sign-in.component';
import { SignUpComponent } from './Authentication/sign-up/sign-up.component';
import { TwoStepAuthComponent } from './Authentication/two-step-auth/two-step-auth.component';
import { ChangeLogComponent } from './Change-log/change-log/change-log.component';
import { AttendanceEmployeesComponent } from './employees/attendance-employees/attendance-employees.component';
import { AttendanceComponent } from './employees/attendance/attendance.component';
import { DepartmentComponent } from './employees/department/department.component';
import { HolidaysComponent } from './employees/holidays/holidays.component';
import { LeaveRequestComponent } from './employees/leave-request/leave-request.component';
import { LoanComponent } from './employees/loan/loan.component';
import { MemberProfileComponent } from './employees/member-profile/member-profile.component';
import { MembersComponent } from './employees/members/members.component';
import { HElpComponent } from './help/h-elp/h-elp.component';
import { HrDashboardComponent } from './hr-dashboard/hr-dashboard.component';
import { ApexChartsComponent } from './Other-Pages/apex-charts/apex-charts.component';
import { ContactComponent } from './Other-Pages/contact/contact.component';
import { FormsComponent } from './Other-Pages/forms/forms.component';
import { IconComponent } from './Other-Pages/icon/icon.component';
import { ReviewComponent } from './Other-Pages/review/review.component';
import { TableComponent } from './Other-Pages/table/table.component';
import { TodoListComponent } from './Other-Pages/todo-list/todo-list.component';
import { WidgetsComponent } from './Other-Pages/widgets/widgets.component';
import { ClientProfileComponent } from './our-clients/client-profile/client-profile.component';
import { ClientsComponent } from './our-clients/clients/clients.component';
import { ProjectDashboardComponent } from './project-dashboard/project-dashboard.component';
import { LeadersComponent } from './projects/leaders/leaders.component';
import { ProjectsComponent } from './projects/projects/projects.component';
import { TasksComponent } from './projects/tasks/tasks.component';
import { TimesheetsComponent } from './projects/timesheets/timesheets.component';
import { FinalStatementComponent } from './Resignation/final-statement/final-statement.component';
import { ResignationComponent } from './Resignation/resignation/resignation.component';
import { StaterPageComponent } from './Starter-page/stater-page/stater-page.component';
import { TicketDetailComponent } from './tickets/ticket-detail/ticket-detail.component';
import { TicketsComponent } from './tickets/tickets/tickets.component';

const routes: Routes = [
  { path: 'hr', component: HrDashboardComponent},
  {path:'project', component:ProjectDashboardComponent},
  {path:'projects', component: ProjectsComponent },
  {path: 'tasks', component: TasksComponent},
  {path:'timesheets', component: TimesheetsComponent },
  {path:'team-leaders', component: LeadersComponent },
  {path: 'tickets', component: TicketsComponent},
  {path:'ticket-detail', component: TicketDetailComponent},
  {path:'ourclients',component: ClientsComponent},
  {path: 'cprofile', component: ClientProfileComponent},
  {path: 'members', component: MembersComponent},
  {path: 'employee-profile', component: MemberProfileComponent},
  {path:'holidays', component: HolidaysComponent},
  {path: 'attendance-employees', component: AttendanceEmployeesComponent},
  {path: 'attendance', component: AttendanceComponent},
  {path: 'leave-request', component: LeaveRequestComponent},
  {path: 'department', component: DepartmentComponent},
  {path: 'loan' , component: LoanComponent},
  {path: 'invoices', component: InvoicesComponent},
  {path: 'payments', component: PaymentsComponent},
  {path: 'expenses', component: ExpensesComponent},
  {path: 'create-invoice', component: CreateInvoiceComponent},
  {path: 'salaryslip', component: EmployeeSalaryComponent},
  {path: 'calendar', component: CalanderComponent},
  {path: 'chat', component: ChatComponent},
  {path: 'charts', component: ApexChartsComponent },
  {path: 'forms', component: FormsComponent },
  {path: 'contact', component: ContactComponent },
  {path: 'table', component: TableComponent },
  {path: 'widgets', component: WidgetsComponent },
  {path: 'icon', component: IconComponent },
  {path: 'review', component: ReviewComponent},
  {path: 'todo-list', component: TodoListComponent },
  {path: 'changelog', component: ChangeLogComponent},
  {path: 'starter-page', component: StaterPageComponent},
  {path: 'help', component: HElpComponent},
  {path: 'auth-signin', component: SignInComponent},
  {path: 'auth-signup', component: SignUpComponent},
  {path: 'auth-password-reset', component: PasswordResetComponent},
  {path: 'auth-two-step', component: TwoStepAuthComponent},
  {path: 'auth-404', component: ErrorComponent},
  {path: 'resign', component: ResignationComponent},
  {path: 'final-statement', component: FinalStatementComponent}

]
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
