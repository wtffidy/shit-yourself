import os
import sys
import subprocess
import tempfile
import venv
import ctypes
import random
import time


def create_temp_venv(temp_dir):
    print("Creating temporary virtual environment...")
    builder = venv.EnvBuilder(with_pip=True)
    builder.create(temp_dir)


def install_pygame_in_venv(temp_dir):
    print("Installing pygame in temporary venv...")
    if os.name == "nt":
        pip_exe = os.path.join(temp_dir, "Scripts", "pip.exe")
    else:
        pip_exe = os.path.join(temp_dir, "bin", "pip")
    subprocess.check_call([pip_exe, "install", "pygame"])


def run_script_in_venv(temp_dir):
    if os.name == "nt":
        python_exe = os.path.join(temp_dir, "Scripts", "python.exe")
    else:
        python_exe = os.path.join(temp_dir, "bin", "python")

    # Relaunch this launcher script inside the venv with a flag to avoid recursion
    subprocess.check_call([python_exe, __file__, "--inside-venv"])


def main_spinner():
    spinner_dir = os.path.join(os.path.dirname(__file__), "Skua Roulette")
    if spinner_dir not in sys.path:
        sys.path.insert(0, spinner_dir)

    try:
        from Spinner_Code import run_spinner
    except ImportError as e:
        print(f"Failed to import run_spinner from Spinner_Code.py: {e}")
        sys.exit(1)

    run_spinner()


def clear_console():
    if os.name == "nt":
        os.system("cls")
    else:
        os.system("clear")


def set_console_colors(bg="0", fg="4"):
    color_code = f"{bg}{fg}"
    os.system(f"color {color_code}")


if __name__ == "__main__":
    if os.name == "nt":
        ctypes.windll.kernel32.SetConsoleTitleW("Skua Script Spinner Launcher")
        clear_console()
        os.system("chcp 65001 > nul")  # Enable UTF-8 console

        for _ in range(3):
            fg_color = random.choice(["4", "C", "E"])
            set_console_colors(bg="0", fg=fg_color)
            print("** Spinning up the Skua Roulette **".center(80))
            time.sleep(0.2)
            clear_console()

        # Final stable color and welcome message
        set_console_colors(bg="0", fg="4")
        print("Welcome to the Skua Script Spinner!".center(80))
        print("Please wait for the application to lauch".center(80))
        print("Then Press SPACE to spin the wheel...".center(80))
        print()

    if "--inside-venv" in sys.argv:
        main_spinner()
    else:
        with tempfile.TemporaryDirectory() as temp_venv_dir:
            create_temp_venv(temp_venv_dir)
            install_pygame_in_venv(temp_venv_dir)
            run_script_in_venv(temp_venv_dir)
