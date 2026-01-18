import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { SessionHelperService } from '../../core/session-helper.service';
import { IColumn } from '../../models/column';
import { IOptions } from '../../models/option';
import { ITeamMember } from '../../models/team-member';
import { TeamService } from '../../services/team.service';
import { Severity } from '../../models/severity';
import { ToastService } from '../../core/ToastServices/toast.service';

@Component({
  selector: 'app-update-dialog',
  standalone: false,
  templateUrl: './update-dialog.component.html',
  styleUrls: ['./update-dialog.component.css'],
})
export class UpdateDialogComponent implements OnInit {
  updateForm!: FormGroup;
  columns: IColumn[] = [];
  team:ITeamMember[]=[];
  statusOptions: IOptions[] = [
    { label: 'Not Started', value: 1 },
    { label: 'In Progress', value: 2 },
    { label: 'Completed', value: 3 }
  ];

  constructor(
    private readonly _fb: FormBuilder,
    private readonly _ref: DynamicDialogRef,
    private readonly _config: DynamicDialogConfig,
    private readonly _sessionService: SessionHelperService,
    private readonly _teamService: TeamService,
    private readonly _toastService: ToastService

  ) {}

  ngOnInit(): void {
    this.getTeamMembers();
    this.initializeForm();
    this.populateFormControls();
  }

  private initializeForm(): void {
    this.updateForm = this._fb.group({});
    this.columns = this._config.data?.columns || [];
  }

  private populateFormControls(): void {
    const entityData = this._config.data?.entity || {};

    this.columns.forEach((col) => {
      this.updateForm.addControl(
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

    const disabled = col.type !== 'status' && col.type !== 'comment' && col.type !=='assign';
    const validators =
      col.type === 'status' || col.type === 'comment'
        ? [Validators.required]
        : [];

    return this._fb.control({ value, disabled }, validators);
  }

  get formControls() {
    return this.updateForm.controls;
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

  saveChanges(): void {
    if (this.updateForm.invalid) {
      this.updateForm.markAllAsTouched();
      return;
    }
    this._ref.close(this.formattedData());
  }

  formattedData(): unknown {
    const formData = this.updateForm.getRawValue();

    const formatData = {
      id: formData.id,
      status: formData.status,
      comment: {
        userName: this._sessionService.get('userName'),
        userId: Number(this._sessionService.get('userId')),
        taskId: formData.id,
        commentMessage: formData.comment,
      }
    };
    return formatData;
  }

  closeDialog(): void {
    this._ref.close(false);
  }

  onUpload(): void{
    
  }
}
