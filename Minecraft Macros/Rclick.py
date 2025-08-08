import threading
import tkinter as tk
from tkinter import ttk
from pynput import keyboard
from pynput.mouse import Button, Controller as MouseController
import time

# Controller
mouse_controller = MouseController()

# State flag
spamming = threading.Event()


def spam_right_click():
    while True:
        if spamming.is_set():
            mouse_controller.click(Button.right)
            time.sleep(0.0001)  # ~200 clicks/sec — adjust as needed
        else:
            time.sleep(0.0001)


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
root.title("Right Click Spammer")
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
threading.Thread(target=spam_right_click, daemon=True).start()
threading.Thread(target=start_listener, daemon=True).start()

root.mainloop()
