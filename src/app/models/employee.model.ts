export interface Employee {
  id: number;
  employeeCode: string;
  firstName: string;
  lastName: string;

  dateOfBirth?: string;
  gender?: string;
  maritalStatus?: string;
  bloodGroup?: string;
  profilePhoto?: string;

  email: string;
  phone?: string;
  emergencyContact?: string;

  address?: string;
  city?: string;
  state?: string;
  pincode?: string;

  departmentId?: number;
  designation?: string;
  managerId?: number;
  joiningDate?: string;
  employmentType?: string;

  salary?: number;
  shift?: string;
  workLocation?: string;

  isActive: boolean;
}
