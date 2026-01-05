namespace Riskuj.Core;

public class Riskuj {
	public Riskuj(string questionPath, params IEnumerable<Team> teams)
	{
		questions = QuestionSystem.FromJson(questionPath);
		this.teams = teams;
	}
	public IEnumerable<Team> teams;
	public QuestionSystem questions;
}

public struct Team(string name, int initialPoints = 0) {
	string name = name;
	int points = initialPoints;
}
