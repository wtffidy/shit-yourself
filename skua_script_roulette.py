import os
import pygame
import random
import math
import sys

# Initialize pygame
pygame.init()
pygame.font.init()

# Constants
WIDTH, HEIGHT = 800, 800
CENTER = (WIDTH // 2, HEIGHT // 2)
RADIUS = 300
FPS = 60
FONT = pygame.font.SysFont("Arial", 24)
WHEEL_SPEED = 0.02  # Controls spin acceleration

# Colors
WHITE = (255, 255, 255)
BLACK = (0, 0, 0)

# Load scripts from user's Skua folder
user_docs = os.path.expanduser("~/Documents")
script_folder = os.path.join(user_docs, "Skua", "Scripts")

script_list = []
for root, _, files in os.walk(script_folder):
    for file in files:
        if file.endswith(".cs"):
            relative_path = os.path.relpath(os.path.join(root, file), script_folder)
            script_list.append(relative_path)

if not script_list:
    print("No scripts found in", script_folder)
    sys.exit()

# Pygame setup
screen = pygame.display.set_mode((WIDTH, HEIGHT))
pygame.display.set_caption("Skua Script Spinner")
clock = pygame.time.Clock()

# Calculate angles for each slice
num_items = len(script_list)
angle_per_item = 2 * math.pi / num_items

# Rotation state
rotation = 0
spin_speed = 0
spinning = False
selected_index = None

def draw_wheel():
    for i, script in enumerate(script_list):
        angle_start = rotation + i * angle_per_item
        angle_end = angle_start + angle_per_item

        # Draw wedge
        point1 = CENTER
        point2 = (CENTER[0] + RADIUS * math.cos(angle_start),
                  CENTER[1] + RADIUS * math.sin(angle_start))
        point3 = (CENTER[0] + RADIUS * math.cos(angle_end),
                  CENTER[1] + RADIUS * math.sin(angle_end))

        color = pygame.Color(0)
        color.hsva = ((i * 360 / num_items) % 360, 100, 100, 100)
        pygame.draw.polygon(screen, color, [point1, point2, point3])

        # Text
        text_angle = angle_start + angle_per_item / 2
        text_x = CENTER[0] + (RADIUS / 1.5) * math.cos(text_angle)
        text_y = CENTER[1] + (RADIUS / 1.5) * math.sin(text_angle)
        label = FONT.render(script.split(os.sep)[-1], True, BLACK)
        rect = label.get_rect(center=(text_x, text_y))
        screen.blit(label, rect)

def get_selected_index():
    # Get the script at the "top" of the wheel (north)
    normalized = (rotation % (2 * math.pi))
    index = int((-normalized + math.pi / 2) % (2 * math.pi) // angle_per_item)
    return index

def draw_pointer():
    pygame.draw.polygon(screen, BLACK, [
        (CENTER[0], CENTER[1] - RADIUS - 20),
        (CENTER[0] - 15, CENTER[1] - RADIUS + 10),
        (CENTER[0] + 15, CENTER[1] - RADIUS + 10)
    ])

# Main loop
running = True
while running:
    screen.fill(WHITE)
    draw_wheel()
    draw_pointer()

    if spinning:
        spin_speed *= 0.985  # Slow down gradually
        if spin_speed < 0.001:
            spin_speed = 0
            spinning = False
            selected_index = get_selected_index()
            selected_script = script_list[selected_index]
            print("Selected:", selected_script)
        else:
            rotation += spin_speed

    # Display selected script
    if selected_index is not None:
        label = FONT.render(f"Selected: {script_list[selected_index]}", True, BLACK)
        screen.blit(label, (20, HEIGHT - 40))

    pygame.display.flip()
    clock.tick(FPS)

    # Handle input
    for event in pygame.event.get():
        if event.type == pygame.QUIT:
            running = False
        elif event.type == pygame.KEYDOWN and event.key == pygame.K_SPACE:
            if not spinning:
                spin_speed = random.uniform(0.4, 0.8)
                spinning = True
                selected_index = None

pygame.quit()
