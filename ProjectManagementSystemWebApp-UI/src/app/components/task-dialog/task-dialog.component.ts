import { Component } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { ToastService } from '../../core/ToastServices/toast.service';
import { IColumn } from '../../models/column';
import { Severity } from '../../models/severity';
import { ITeamMember } from '../../models/team-member';
import { TeamService } from '../../services/team.service';

@Component({
  selector: 'app-task-dialog',
  standalone: false,
  templateUrl: './task-dialog.component.html',
  styleUrl: './task-dialog.component.css',
})
export class TaskDialogComponent {
  addForm!: FormGroup;
  columns: IColumn[] = [];
  team: ITeamMember[] = [];

  constructor(
    private readonly _fb: FormBuilder,
    private readonly _ref: DynamicDialogRef,
    private readonly _config: DynamicDialogConfig,
    private readonly _teamService: TeamService,
    private readonly _toastService: ToastService
  ) {}

  ngOnInit(): void {
    this.getTeamMembers();
    this.initializeForm();
    this.populateFormControls();
  }

  private getTeamMembers(): void {
    this._teamService.fetchTeamMembers().subscribe({
      next: (teamMember: ITeamMember[]) => {
        this.team = teamMember;
      },
      error: (error) => {
        this._toastService.showToast(
          Severity.error,
          'Error fetching Task Teams:',
          error.message
        );
      }
    });
  }
  private initializeForm(): void {
    this.addForm = this._fb.group({});
    this.columns = this._config.data?.columns || [];
  }

  private populateFormControls(): void {
    const entityData = this._config.data?.entity || {};

    this.columns.forEach((col) => {
      this.addForm.addControl(
        col.field,
        this.createFormControl(col, entityData)
      );
    });
  }

  private createFormControl(col: IColumn, entityData: any): FormControl {
    let value: string | Date = '';

    if (col.type === 'date' && entityData[col.field]) {
      value = new Date(entityData[col.field]);
    } else if (col.type !== 'comment') {
      value = entityData[col.field] || '';
    }

    const disabled = col.type !== 'assign' && col.type !== 'comment';
    const validators =
      col.type === 'status' || col.type === 'comment'
        ? [Validators.required]
        : [];

    return this._fb.control({ value, disabled }, validators);
  }

  get formControls() {
    return this.addForm.controls;
  }

  saveChanges(): void {
    if (this.addForm.invalid) {
      this.addForm.markAllAsTouched();
      return;
    }
    this._ref.close(this.formattedData());
  }

  formattedData(): unknown {
    const formData = this.addForm.getRawValue();
    const formatData = {
      taskId: formData.id,
      teamMemberId: formData.assign,
    };
    return formatData;
  }

  closeDialog(): void {
    this._ref.close(false);
  }
}
