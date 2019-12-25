export interface IFormArea {
    formAreaId?: number;
    name?: string;
    formCount?: number;
    created?: Date;
    createdBy: string;
}

export interface IForm {
    formId?: number;
    name?: string;
    description?: string;
    created: Date;
    updated: Date;
    createdBy: string;
    updatedBy: string;
}

export interface IFormItem {
    formItemId?: number;
    name?: string;
    timeStamp?: string;
    created: Date;
    updated: Date;
    createdBy: string;
    updatedBy: string;
}
