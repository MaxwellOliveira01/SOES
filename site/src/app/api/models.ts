export interface IdentificationRequest {
  email: string;
  sendOtp: boolean;
}

export interface IdentificationResponse {
  success: boolean;
  errorMessage?: string;
  name: string;
  session: string;
}

export interface AuthenticationRequest {
  otp: string;
  session: string;
}

export interface AuthenticationResponse {
  success: boolean;
  errorMessage?: string;
  session?: string;
  elections: ElectionVoterModel[];
}

export interface ElectionVoterModel {
  id: string;
  name: string;
  hasVoted: boolean;
}
