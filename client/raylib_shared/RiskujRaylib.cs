using Raylib_cs;
using static Raylib_cs.Raylib;
namespace Riskuj.RaylibShared;

public class RiskujRaylib : Core.Riskuj
{
    public RiskujRaylib(string questionPath) : base(questionPath, [])
    {
        mainScreen = new MainScreen(this);
        questionScreen = new QuestionScreen(this);
        current_screen = mainScreen;
    }
    public MainScreen mainScreen;
    public QuestionScreen questionScreen;
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
    }
    public void Draw(float dt)
    {
        current_screen.Draw(dt);
    }
}

public interface IScreen
{
    public abstract void Init();
    public abstract void Update(float dt);
    public abstract void Draw(float dt);
}
