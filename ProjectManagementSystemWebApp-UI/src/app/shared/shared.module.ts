import { CommonModule } from '@angular/common';
import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AgGridAngular, AgGridModule } from 'ag-grid-angular';
import { AllCommunityModule, ModuleRegistry } from 'ag-grid-community';
import { ButtonModule } from 'primeng/button';
import { TableModule } from 'primeng/table';
import { TagModule } from 'primeng/tag';
import { ToggleButtonModule } from 'primeng/togglebutton';
import { ToggleSwitch, ToggleSwitchModule } from 'primeng/toggleswitch';
import { ParseStatusPipe } from '../pipes/parse-status.pipe';

import { TooltipModule } from 'primeng/tooltip';
import { ActionButtonComponent } from './cellRenderers/action-button/action-button.component';
import { AssignButtonComponent } from './cellRenderers/assign-button/assign-button.component';
import { DateRendererComponent } from './cellRenderers/date-renderer/date-renderer.component';
import { StatusRendererComponent } from './cellRenderers/status-renderer/status-renderer.component';
import { ViewDetailComponent } from './cellRenderers/view-detail/view-detail.component';
import { ProgressBarComponent } from './progress-spinner/progress-bar/progress-bar.component';
import { TableGridComponent } from './table-grid/table-grid.component';
import { TableComponent } from './table/table.component';
import { WelcomeScreenComponent } from './welcome-screen/welcome-screen.component';
import 'ag-grid-enterprise'
ModuleRegistry.registerModules([AllCommunityModule]);

@NgModule({
  declarations: [
    TableComponent,
    TableGridComponent,
    ActionButtonComponent,
    ProgressBarComponent,
    DateRendererComponent,
    StatusRendererComponent,
    WelcomeScreenComponent,
    AssignButtonComponent,
    ViewDetailComponent,
  ],
  imports: [
    CommonModule,
    ToggleSwitchModule,
    FormsModule,
    TableModule,
    TagModule,
    ToggleSwitch,
    ToggleButtonModule,
    FormsModule,
    ParseStatusPipe,
    ButtonModule,
    AgGridAngular,
    AgGridModule,
    TooltipModule,
  ],
  exports: [
    TableComponent,
    TableGridComponent,
    ActionButtonComponent,
    ProgressBarComponent,
    DateRendererComponent,
    WelcomeScreenComponent,
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class SharedModule {}
