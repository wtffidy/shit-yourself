import tkinter as tk
from tkinter import ttk
from PIL import Image, ImageTk
import requests
from io import BytesIO
import pyperclip

def download_image(url):
    response = requests.get(url)
    image = Image.open(BytesIO(response.content))
    return image

def save_as_ico(image, filename="temp.ico"):
    image.save(filename, format="ICO", sizes=[(32, 32)])
    return filename

def apply_role(person_name, role):
    role_command = f"-role {person_name} {role}"
    pyperclip.copy(role_command)
    print(f"Copied: {role_command}")

def create_role_widgets(frame, person_name, roles):
    for role, description in roles.items():
        role_label = ttk.Label(frame, text=f"🥔 {role}", font=("Helvetica", 12, "bold"))
        role_label.pack(pady=5)

        description_label = ttk.Label(frame, text=description, font=("Helvetica", 10))
        description_label.pack(pady=5)

        # Adding a custom style for the "Copy" button with a different text color
        copy_button = ttk.Button(frame, text="Copy Role Command", command=lambda r=role: apply_role(person_name.get(), r),
                                  style="Copy.TButton")
        copy_button.pack(pady=5)

def update_background_color(window, target_color, current_color, step, delay):
    def interpolate(c1, c2, t):
        return int(c1[0] * (1 - t) + c2[0] * t), int(c1[1] * (1 - t) + c2[1] * t), int(c1[2] * (1 - t) + c2[2] * t)

    def update_color():
        nonlocal current_color
        if current_color != target_color:
            current_color = interpolate(current_color, target_color, step)
            window.configure(bg="#%02x%02x%02x" % current_color)
            window.after(delay, update_color)

    update_color()

def main():
    roles = {
        "Apprentice of War": "Own 4 or more accounts at Level 100.\nHave 1 solid DPS, Farmer, and Support class on each of the 4 accounts.",
        "Master of War": "Achieve the requirements of the Apprentice of War.\nPossess a weapon with +30% damage to all monsters on 4 or more accounts.\nHave a non-enhance-able item with +30% damage to 4 or more monster types on 4 or more accounts.",
        "Apostle of War": "Achieve the requirements of Master of War.\nShow Ultra Ezrajal, Ultra Warden, and Ultra Engineer insignias in army inventories,\nOR provide a recording of your army killing them.",
        "Bishop of War": "Achieve the requirements of Apostle of War.\nHave 1 +51% weapon role (i.e., Legatus of War).\nHave 1 Class-based role (i.e., ArchMage of War).\nShow Ultra Dage/Nulgath insignias in army inventories,\nOR provide a recording of your army killing them.",
        "Cardinal of War": "Achieve the requirements of Bishop of War.\nHave 1 Weapon, Helm, and Cape Forge Enhancement unlocked.\nHave 4 +51% weapon roles (Legatus of War, Deacon of War counts as 2 each).\nHave 4 Class-based roles (ArchMage of War).\nShow Dark Carnax char page badge,\nOR provide a recording of your army killing Dark Carnax.",
        "God of War": "Have all army roles, excluding pay-to-win ones such as Time Lord of War,\nor punitive ones such as Casualty of War.",
        "Emperor of War": "Own 10 or more accounts with Master of War requirements.",
        "Victor of War": "Have the Valiance Forge enhancement unlocked on 4 or more accounts.",
        "Conductor of War": "Have the Arcana’s Concerto forge enhancement unlocked on 4 or more accounts.",
        "Deliverance of War": "Have the Elysium forge enhancement unlocked on 4 or more accounts.",
        "Reflectionist of War": "Have the Examen forge enhancement unlocked on 4 or more accounts.",
        "Penitent of War": "Have the Penitence forge enhancement unlocked on 4 or more accounts.",
        "Ascendant of War": "Have the Awescended set on 4 or more accounts.",
        "Primordial of War": "Have Necrotic Sword of Doom on 4 or more accounts.",
        "Wraith of War": "Have Hollowborn Sword of Doom on 4 or more accounts.",
        "Avenger of War": "Have the Chaos Avenger class on 4 or more accounts.",
        "Prudence of War": "Have Providence on 4 or more accounts.",
        "ArchMage of War": "Have the ArchMage class on 4 or more accounts.",
        "Legatus of War": "Have Necrotic Blade of the Underworld on 4 or more accounts.",
        "Revenant of War": "Have the Legion Revenant class on 4 or more accounts.",
        "Chauvinist of War": "Have Necrotic Sword of the Abyss on 4 or more accounts.",
        "Sinner of War": "Have Sin of the Abyss on 4 or more accounts.",
        "HighLord of War": "Have the Void HighLord class on 4 or more accounts.",
        "Deacon of War": "Have Exalted Apotheosis and Dual Apotheosis on 4 or more accounts.",
        "Eternal Dragon of War": "Have the Dragon of Time class on 4 or more accounts.",
        "Prisoner of War": "Have Hollowborn Reaper's Scythe on 4 or more accounts.",
        "Time Lord of War": "Have a Calendar/Chrono class on 4 or more accounts.",
        "Casualty of War": "Have 4 or more banned accounts at a time, includes permanent and temporary bans.",
        "Vera of War": "Have the Verus DoomKnight class on 4 or more accounts.",
        "Radiant Goddess of War": "Have 4 Rgow on 4 or more accounts."
    }

    window = tk.Tk()
    window.title("Role Giver [Moderator+]")

    # Replace "https://i.imgur.com/geLMHIm.jpg" with the actual URL of your ICO image
    icon_url = "https://i.imgur.com/geLMHIm.jpg"
    icon_image = download_image(icon_url)

    # Save the image as ICO file
    icon_filename = save_as_ico(icon_image)

    # Set the application icon
    window.iconbitmap(default=icon_filename)

    # Download the image from the URL
    image_url = "https://i.imgur.com/u0CJmOc.jpg"
    response = requests.get(image_url)
    image = Image.open(BytesIO(response.content))
    photo = ImageTk.PhotoImage(image)

    # Create a label to display the program logo
    logo_label = tk.Label(window, image=photo)
    logo_label.photo = photo
    logo_label.pack()

    person_name_label = ttk.Label(window, text="Insert Name or UID:")
    person_name_label.pack(pady=10)

    person_name = tk.StringVar()
    person_name_entry = ttk.Entry(window, textvariable=person_name)
    person_name_entry.pack(pady=10)

    canvas = tk.Canvas(window)
    canvas.pack(side=tk.LEFT, fill=tk.BOTH, expand=True)

    scrollbar = ttk.Scrollbar(window, orient="vertical", command=canvas.yview)
    scrollbar.pack(side=tk.RIGHT, fill=tk.Y)

    frame = ttk.Frame(canvas, style="My.TFrame")
    canvas.create_window((0, 0), window=frame, anchor="nw")

    create_role_widgets(frame, person_name_entry, roles)

    frame.bind("<Configure>", lambda e: canvas.configure(scrollregion=canvas.bbox("all")))
    canvas.configure(yscrollcommand=scrollbar.set)

    # Adding a custom style for the frame
    window.style = ttk.Style()
    window.style.configure("My.TFrame", background="#fff")

    # Start smoothly fading background color
    target_color = (255, 255, 255)  # White background
    current_color = (255, 255, 255)  # Initial color (white)
    update_background_color(window, target_color, current_color, step=0.01, delay=10)

    # Adding a custom style for the "Copy" button with a different text color
    window.style.configure("Copy.TButton", font=("Helvetica", 10), foreground="#333", background="#4CAF50")

    # Bind mouse wheel event to canvas for scrolling
    canvas.bind_all("<MouseWheel>", lambda event: canvas.yview_scroll(int(-1 * (event.delta / 120)), "units"))

    window.mainloop()

if __name__ == "__main__":
    main()
