using System;
using System.Threading.Tasks;

namespace TestTaskRemoteRestApi
{
    public class MenuOptions
    {
        private bool RequestedExit;
        public async Task RunApplicationAsync()
        {

            while (!RequestedExit)
            {
                ShowCommands();
                await WaitForCommandAsync();
            }
        }

        private async Task WaitForCommandAsync()
        {
            ShowMessage("> ");
            int command;

            while (!int.TryParse(Console.ReadLine(), out command))
            {
                ShowMessage($"Command:{command} doesn't exist\n");
            }

            await ApplyCommandAsync(command);
        }

        public async Task ApplyCommandAsync(int command)
        {
            switch ((Options)command)
            {

                case Options.Start:
                    GetDataFromRemoteApi result = new GetDataFromRemoteApi("https://tester.consimple.pro");
                    await result.DeserelizationAsync();
                    break;

                case Options.Exit:
                    RequestedExit = true;
                    break;

                default:
                    ShowMessage($"Command:{command} doesn't exist\n");
                    break;
            }
        }

        static void ShowCommands()
        {
            ShowMessage(
                $"1) {Options.Start} - Get request from remote Api.",
                $"2) {Options.Exit} - Exit application.");
        }

        static void ShowMessage(params string[] msgs)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            foreach (var msg in msgs)
            {
                Console.WriteLine(msg);
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
