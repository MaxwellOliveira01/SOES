import { ElectionVoterModel } from "../api/models";

export interface VoterData {
  voterName: string;
  session: string;
  elections: ElectionVoterModel[];
}
