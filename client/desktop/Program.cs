using Riskuj.RaylibShared;

RiskujRaylib riskuj = new RiskujRaylib(args.ElementAtOrDefault(0) ?? AppDomain.CurrentDomain.BaseDirectory + "questions.json");
riskuj.Run();