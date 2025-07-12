namespace SOE.Api;

public class ElectionFullModel : ElectionVoterModel {
    public bool HasVoted { get; set; }

    public List<OptionModel> Options { get; set; }

}