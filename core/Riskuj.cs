namespace Riskuj.Core;

public class Riskuj {
	public Riskuj(string questionPath, params Team[] teams)
	{
		questions = QuestionSystem.FromJson(questionPath);
		this.teams = teams;
	}
	public Team[] teams;
	public QuestionSystem questions;
}

public class Team(string name, int initialPoints = 0) {
	public string name = name;
	public int points = initialPoints;
}
