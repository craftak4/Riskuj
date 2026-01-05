using System.Numerics;
using Raylib_cs;
using Riskuj.Core;
using static Raylib_cs.Raylib;
namespace Riskuj.RaylibShared;

public class RiskujRaylib : Core.Riskuj
{
    public RiskujRaylib(string questionPath) : base(questionPath, [
        new Team("Team1"),
        new Team("Team2"),
        new Team("Team3"),
        new Team("Team4"),
    ])
    {
        mainScreen = new MainScreen(this);
        questionScreen = new QuestionScreen(this);
        current_screen = mainScreen;
        teamOverlay = new(this);
    }
    public MainScreen mainScreen;
    public QuestionScreen questionScreen;
    public TeamOverlay teamOverlay;
    public IScreen current_screen;
    public void Run()
    {
#if !DEBUG
        SetTraceLogLevel(TraceLogLevel.Error | TraceLogLevel.Fatal);
#endif
        InitWindow(1920, 1080, "Riskuj - Vít Kadlec");
        SetWindowState(ConfigFlags.ResizableWindow);
        MaximizeWindow();


        current_screen.Init();
        teamOverlay.Init();

        while (!WindowShouldClose())
        {
            PollInputEvents();

            Update(GetFrameTime());

            BeginDrawing();
            {
                Draw(GetFrameTime());
                ClearBackground(Color.Black);
            }
            EndDrawing();
        }

        CloseWindow();
    }
    public void Update(float dt)
    {
        current_screen.Update(dt);
        teamOverlay.Update();
    }
    public void Draw(float dt)
    {
        current_screen.Draw(dt);
        teamOverlay.Draw();
    }
}

public interface IScreen
{
    public abstract void Init();
    public abstract void Update(float dt);
    public abstract void Draw(float dt);
}

public class TeamOverlay(RiskujRaylib riskuj)
{
    List<TextBox> textBoxes = new();
    List<Button> buttons = new();
    public void Update()
    {
        foreach (Button button in buttons)
            button.Update();
    }
    public void Draw()
    {
        foreach (TextBox box in textBoxes)
            box.Draw();
        foreach (Button button in buttons)
            button.Draw();
    }
    public void Init()
    {
        textBoxes = new();
        buttons = new();
        Vector2 screenSize = new Vector2(GetScreenWidth(), GetScreenHeight());
        Vector2 size = new Vector2(screenSize.X / 10f, screenSize.Y / 16f);
        Vector2 buttonSize = new Vector2(size.Y, size.Y);
        int i = 1;
        if (riskuj.current_screen == riskuj.questionScreen)
            foreach (Team team in riskuj.teams)
            {
                textBoxes.Add(new TextBox()
                {
                    rectangle = new Rectangle(screenSize.X / 2 - size.X / 2 - buttonSize.X, screenSize.Y - i * size.Y, size),
                    color = Color.Blue,
                    text = $"{team.name}: {team.points}"
                });
                buttons.Add(new Button()
                {
                    rectangle = new Rectangle(screenSize.X / 2 + size.X / 2 - buttonSize.X, screenSize.Y - i * buttonSize.Y, buttonSize),
                    color = Color.Green,
                    text = "+",
                    onPressed = () => { team.points += riskuj.questionScreen.question.points; Init(); }
                });
                buttons.Add(new Button()
                {
                    rectangle = new Rectangle(screenSize.X / 2 + size.X / 2, screenSize.Y - i * buttonSize.Y, buttonSize),
                    color = Color.Red,
                    text = "-",
                    onPressed = () => { team.points -= riskuj.questionScreen.question.points; Init(); }
                });
                i++;
            }
        else
            foreach (Team team in riskuj.teams)
            {
                textBoxes.Add(new TextBox()
                {
                    rectangle = new Rectangle(screenSize.X / 2 - size.X / 2, screenSize.Y - i * size.Y, size),
                    color = Color.Blue,
                    text = $"{team.name}: {team.points}"
                });
                i++;
            }
    }
}