export class AddOrEditUserModel {
  id?: number;
  email!: string;
  firstName!: string;
  lastName!: string;
  roleId!: number;
  name?: string;
  password?: string;
}

export class UpdateProfileModel {
  email!: string;
  firstName!: string;
  lastName!: string;
  newPassword?: string;
  confirmPassword?: string;
}

export default class UserModel {
  id?: number;
  email!: string;
  firstname!: string;
  lastname!: string;
  roleid!: number;
}
