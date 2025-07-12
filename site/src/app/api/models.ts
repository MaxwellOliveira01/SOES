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

export interface OptionModel {
  id: string;
  name: string;
}

export interface ElectionVoterModel {
  id: string;
  name: string;
}

export interface ElectionFullModel extends ElectionVoterModel {
  options: OptionModel[];
  hasVoted: boolean;
}

export interface VoterElectionModelRequest {
  electionId: string;
  session: string;
}

export interface SubmitVoteRequest {
  session: string;
  electionId: string;
  optionId: string;
  publicKeyPem: string;
  signature: string;
}
