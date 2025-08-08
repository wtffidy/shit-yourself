import threading
import tkinter as tk
from tkinter import ttk
from pynput import keyboard
from pynput.keyboard import Key, Controller as KeyboardController
from pynput.mouse import Button, Controller as MouseController
import time

# Controllers
keyboard_controller = KeyboardController()
mouse_controller = MouseController()

# State flag
spamming = threading.Event()


def spam_tab_shift_leftclick():
    while True:
        if spamming.is_set():
            # Press Tab
            keyboard_controller.press(Key.tab)
            keyboard_controller.release(Key.tab)
            time.sleep(0.5)  # Delay after Tab

            # Shift + Left Click
            keyboard_controller.press(Key.shift)
            mouse_controller.press(Button.left)
            mouse_controller.release(Button.left)
            keyboard_controller.release(Key.shift)

            time.sleep(0.5) # Delay after click
        else:
            time.sleep(0.05)


def toggle_spam():
    if spamming.is_set():
        spamming.clear()
        update_ui("Stopped", "Start Spamming")
    else:
        spamming.set()
        update_ui("Spamming", "Stop Spamming")


def update_ui(status_text, button_text):
    status_var.set(status_text)
    toggle_button.config(text=button_text)


def on_press(key):
    if key == keyboard.Key.f6:
        toggle_spam()


def start_listener():
    listener = keyboard.Listener(on_press=on_press)
    listener.start()


def stop():
    spamming.clear()
    root.destroy()


# --- GUI Setup ---
root = tk.Tk()
root.title("Tab > Shift+Left Click Spammer")
root.geometry("300x140")
root.resizable(False, False)

style = ttk.Style()
style.configure("TButton", font=("Segoe UI", 10))
style.configure("TLabel", font=("Segoe UI", 12))

main = ttk.Frame(root, padding=20)
main.pack(fill="both", expand=True)

ttk.Label(main, text="Status:").pack(anchor="center")
status_var = tk.StringVar(value="Stopped")
status_label = ttk.Label(
    main, textvariable=status_var, foreground="#0078D7", font=("Segoe UI", 14, "bold")
)
status_label.pack(pady=4)

toggle_button = ttk.Button(main, text="Start Spamming", command=toggle_spam)
toggle_button.pack(pady=(10, 4), ipadx=10)

exit_button = ttk.Button(main, text="Exit", command=stop)
exit_button.pack(pady=(0, 4), ipadx=10)

# --- Threads ---
threading.Thread(target=spam_tab_shift_leftclick, daemon=True).start()
threading.Thread(target=start_listener, daemon=True).start()

root.mainloop()
