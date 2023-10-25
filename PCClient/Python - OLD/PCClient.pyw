import atexit
import ctypes
import os
import sys
import time
from datetime import datetime
import keyboard
import requests
from PIL import ImageTk
from cryptography.fernet import Fernet
import threading
from pathlib import Path
from tkinter import Tk, Canvas, Entry, Text, Button, PhotoImage, Toplevel, Label, CENTER
import pystray
import PIL.Image
import tkinter as tk

# Define the overlay function
def block_keyboard(event):
    # Block all keyboard events while overlay is active
    return False

def block_all_keys():
    for i in range(150):
        keyboard.block_key(i)

def unblock_all_keys():
    for i in range(150):
        keyboard.unblock_key(i)

def show_overlay(reminder_text, root):
    overlay = tk.Toplevel(root)
    overlay.attributes("-topmost", True)
    overlay.attributes("-alpha", 0.5)
    overlay.attributes("-fullscreen", True)
    overlay.configure(bg="red")
    OUTPUT_PATH = Path(__file__).parent
    ASSETS_PATH = OUTPUT_PATH / Path(r"assets\frame0")

    def relative_to_assets(path: str) -> Path:
        return ASSETS_PATH / Path(path)

    ButtonBackground = PhotoImage(
        file=relative_to_assets("button_1.png"))


    label = tk.Label(overlay, text=reminder_text, fg="white", bg="red", font=("Helvetica", 32))
    label.place(relx=0.5, rely=0.5, anchor=CENTER, y=-100)
    photoimage = ImageTk.PhotoImage(file="dismiss button.png")

    button = tk.Button(overlay,image=photoimage, borderwidth=0, highlightthickness=0, text="Dismiss", command=lambda: close_overlay(overlay, root), relief="flat")
    # button.pack(pady=10)
    button.place(relx=.5, rely=.5,anchor= CENTER)
    # Block keyboard events
    keyboard.hook(block_keyboard)
    block_all_keys()
    overlay.mainloop()
def close_overlay(overlay,root):
    # Unhook the keyboard event, unblock keys, and destroy the overlay
    keyboard.unhook_all()

    # unblock_all_keys()
    overlay.destroy()
    root.destroy()

def Sleep():
    ctypes.windll.PowrProf.SetSuspendState(0, 0, 0)


def Hibernate():
    os.system(r'rundll32.exe powrprof.dll,SetSuspendState Hibernate')


def Shutdown():
    os.system("shutdown /s /t 1")


def Lock():
    ctypes.windll.user32.LockWorkStation()


def Signout():
    os.system("shutdown -l")


# Check for single instance using a PID file
def main():
    try:
        import psutil
    except ImportError:
        psutil = None

    pidfile = "PCClient.pid"

    def is_process_running(pid):
        if psutil is None:
            return False

        try:
            process = psutil.Process(pid)
            return process.is_running()
        except psutil.NoSuchProcess:
            return False

    try:
        with open(pidfile, 'r') as f:
            pid = int(f.read().strip())
            if is_process_running(pid):
                # Terminate the new instance of the program
                sys.exit(0)
    except FileNotFoundError:
        pass

    # Write the current process's PID to the PID file
    with open(pidfile, 'w') as f:
        f.write(str(os.getpid()))


    def setup_tray_icon():
        global icon

        image=PIL.Image.open("PCClient tray icon.ico")

        def LogOut(icon, item):
            files_to_delete = ["encryption_key.key", "encrypted_data.enc"]

            for filename in files_to_delete:
                if os.path.exists(filename):
                    try:
                        os.remove(filename)
                        print(f"Deleted {filename}")
                    except Exception as e:
                        print(f"Error deleting {filename}: {e}")
            loggedIn = False
            startOfFile()
        def Exit(icon, item):
            icon.stop()
            os._exit(0)

        icon=pystray.Icon("Logo", image, menu=pystray.Menu(

      # pystray.MenuItem("Log Out", LogOut),
            pystray.MenuItem("Exit", Exit),

            ))
        icon.run()

    def generate_or_load_key():
        if os.path.exists('encryption_key.key'):
            with open('encryption_key.key', 'rb') as key_file:
                encryption_key = key_file.read()
        else:
            encryption_key = Fernet.generate_key()
            with open('encryption_key.key', 'wb') as key_file:
                key_file.write(encryption_key)
        return encryption_key

    def encrypt_data(data, cipher_suite):
        encrypted_data = cipher_suite.encrypt(data.encode())
        return encrypted_data

    def decrypt_data(encrypted_data, cipher_suite):
        decrypted_data = cipher_suite.decrypt(encrypted_data).decode()
        return decrypted_data

    def save_encrypted_data(encrypted_data):
        with open('encrypted_data.enc', 'wb') as f:
            f.write(encrypted_data)

    def get_encrypted_data():
        if os.path.exists('encrypted_data.enc'):
            with open('encrypted_data.enc', 'rb') as f:
                encrypted_data = f.read()
            encryption_key = generate_or_load_key()
            cipher_suite = Fernet(encryption_key)

            decrypted_data = decrypt_data(encrypted_data, cipher_suite)
            running(decrypted_data)
        else:
            loginWindowFunction()
            return None

    def addAPI(api_key):
        encryption_key = generate_or_load_key()
        cipher_suite = Fernet(encryption_key)

        encrypted_data = encrypt_data(api_key, cipher_suite)
        save_encrypted_data(encrypted_data)

    operations = ['Sleep', 'Hibernate', 'Shutdown', 'Lock', 'Signout']

    currentTimeURL = "PHP_URL/time.php"
    updateTimeURL = "PHP_URL/pcclient/updatetimedatabase.php"
    findRequests = "PHP_URL/pcclient/findrequests.php"
    updateRequest = "PHP_URL/pcclient/updaterequest.php"
    loginURL = "PHP_URL/login.php"
    getReminder = "PHP_URL/pcclient/getreminder.php"
    complete_url = "PHP_URL/pcclient/completereminder.php"

    def loginWindowFunction():
        OUTPUT_PATH = Path(__file__).parent
        ASSETS_PATH = OUTPUT_PATH / Path(r"assets\frame0")


        def relative_to_assets(path: str) -> Path:
            return ASSETS_PATH / Path(path)


        LoginWindow = Tk()

        LoginWindow.geometry("452x306")
        LoginWindow.configure(bg = "#03253B")
        LoginWindow.title("PCClient")
        LoginWindow.iconbitmap('PCClient Control Box Logo.ico')

        canvas = Canvas(
            LoginWindow,
            bg = "#03253B",
            height = 306,
            width = 452,
            bd = 0,
            highlightthickness = 0,
            relief = "ridge"
        )

        canvas.place(x = 0, y = 0)
        usernameImage = PhotoImage(
            file=relative_to_assets("entry_1.png"))
        usernameBackground = canvas.create_image(
            275.5,
            107.0,
            image=usernameImage
        )
        usernameEntry = Entry(
            bd=0,
            bg="#FFFFFF",
            fg="#000716",
            highlightthickness=0
        )
        usernameEntry.place(
            x=198.0,
            y=90.0,
            width=155.0,
            height=34.0
        )

        passwordImage = PhotoImage(
            file=relative_to_assets("entry_2.png"))
        passwordBackground = canvas.create_image(
            275.5,
            172.0,
            image=passwordImage
        )
        passwordEntry = Entry(
            LoginWindow,show="*",width=20,
            bd=0,
            bg="#FFFFFF",
            fg="#000716",
            highlightthickness=0
        )
        passwordEntry.pack()
        passwordEntry.place(
            x=198.0,
            y=155.0,
            width=155.0,
            height=34.0
        )

        loginButtonBackground = PhotoImage(
            file=relative_to_assets("button_1.png"))
        loginButton = Button(
            image=loginButtonBackground,
            borderwidth=0,
            highlightthickness=0,
            command=lambda: login(usernameEntry, passwordEntry, LoginWindow),
            relief="flat"
        )
        loginButton.place(
            x=135.0,
            y=223.0,
            width=182.0,
            height=55.0
        )

        canvas.create_text(
            86.0,
            89.0,
            anchor="nw",
            text="Username",
            fill="#FFFFFF",
            font=("Inter", 20 * -1)
        )

        canvas.create_text(
            90.0,
            154.0,
            anchor="nw",
            text="Password",
            fill="#FFFFFF",
            font=("Inter", 20 * -1)
        )

        canvas.create_text(
            179.0,
            14.0,
            anchor="nw",
            text="Login",
            fill="#FFFFFF",
            font=("Inter", 36 * -1)
        )
        LoginWindow.resizable(False, False)
        LoginWindow.mainloop()



    def login(usernameEntry, passwordEntry, LoginWindow):
        username=usernameEntry.get()
        password=passwordEntry.get()
        loginparameters = {"loginUsername": username, "loginPassword": password}
        login_response = requests.post(loginURL, data=loginparameters)
        # Check if login was successful before proceeding
        if login_response.text != "Invalid username or password.":
            LoginWindow.destroy()
            loginSuccessFunction()

            api_key = login_response.text
            addAPI(api_key)
            running(api_key)


        else:
            LoginWindow.destroy()
            loginFailedFunction()
    def running(api_key):
        try:
            while True:

                response = requests.get(currentTimeURL)
                data = response.json()
                current_time = str(data["time"])

                updateTimeURLParams = {'timePython': current_time}

                headers = {'X-API-KEY': api_key}
                requests.post(updateTimeURL, data=updateTimeURLParams, headers=headers)

                time.sleep(1)  # You can adjust the delay (in seconds) as needed

                records_response = requests.get(findRequests, headers=headers)
                records = records_response.text

                response = requests.get(getReminder, headers=headers)
                if response.status_code == 200:
                    reminders = response.json()
                    current_date_time = datetime.now()

                    for reminder in reminders:
                        reminder_date_str = reminder["date"]
                        reminder_time_str = reminder["time"]
                        reminder_id = reminder["id"]
                        if reminder_date_str and reminder_time_str:
                            reminder_date = datetime.strptime(reminder_date_str + " " + reminder_time_str,
                                                              "%d/%m/%Y %H:%M")
                            if reminder_date <= current_date_time:
                                reminder=reminder["reminder"]
                                print("Reminder:", reminder)

                                # Show the overlay with the reminder text
                                root = tk.Tk()
                                root.attributes("-fullscreen", True)
                                root.title("Reminder Overlay")

                                show_overlay(reminder, root)

                                root.mainloop()

                                # Send POST request to mark reminder as completed
                                complete_data = {"id": reminder_id}
                                complete_headers = {"X-API-Key": api_key}
                                complete_response = requests.post(complete_url, data=complete_data,
                                                                  headers=complete_headers)

                                if complete_response.status_code == 200:
                                    print("Reminder marked as completed.")
                                else:
                                    print("Failed to mark reminder as completed.")
                        else:
                            print("Reminder has missing date or time:", reminder)
                else:
                    print("Request failed with status code:", response.status_code)

                if records in operations:
                    requests.post(updateRequest, headers=headers)

                    exec(records + "()")

        except requests.exceptions.ConnectionError as e:
            running(api_key)


    def loginSuccessFunction():
        OUTPUT_PATH = Path(__file__).parent
        ASSETS_PATH = OUTPUT_PATH / Path(r"assets\frame1")

        def relative_to_assets(path: str) -> Path:
            return ASSETS_PATH / Path(path)

        loginSuccessWindow = Tk()

        loginSuccessWindow.geometry("440x172")
        loginSuccessWindow.configure(bg="#03253B")
        loginSuccessWindow.title("PCClient")
        loginSuccessWindow.iconbitmap('PCClient Control Box Logo.ico')

        canvas = Canvas(
            loginSuccessWindow,
            bg="#03253B",
            height=172,
            width=440,
            bd=0,
            highlightthickness=0,
            relief="ridge"
        )

        canvas.place(x=0, y=0)
        canvas.create_text(
            148.0,
            14.0,
            anchor="nw",
            text="Success",
            fill="#FFFFFF",
            font=("Inter", 36 * -1)
        )

        canvas.create_text(
            66.0,
            74.0,
            anchor="nw",
            text="You have successfully logged in!",
            fill="#FFFFFF",
            font=("Inter", 20 * -1)
        )

        button_image_1 = PhotoImage(
            file=relative_to_assets("button_1.png"))
        OkButton = Button(
            image=button_image_1,
            borderwidth=0,
            highlightthickness=0,
            command=lambda: loginSuccessWindow.destroy(),
            relief="flat"
        )
        OkButton.place(
            x=151.0,
            y=114.0,
            width=140.0,
            height=40.0
        )
        loginSuccessWindow.resizable(False, False)
        loginSuccessWindow.mainloop()

    def loginFailedFunction():
        OUTPUT_PATH = Path(__file__).parent
        ASSETS_PATH = OUTPUT_PATH / Path(r"assets\frame2")

        def relative_to_assets(path: str) -> Path:
            return ASSETS_PATH / Path(path)

        loginFailedWindow = Tk()

        loginFailedWindow.geometry("478x193")
        loginFailedWindow.configure(bg="#03253B")
        loginFailedWindow.title("PCClient")
        loginFailedWindow.iconbitmap('PCClient Control Box Logo.ico')

        canvas = Canvas(
            loginFailedWindow,
            bg="#03253B",
            height=193,
            width=478,
            bd=0,
            highlightthickness=0,
            relief="ridge"
        )

        canvas.place(x=0, y=0)
        canvas.create_text(
            136.0,
            9.0,
            anchor="nw",
            text="Login Failed",
            fill="#FFFFFF",
            font=("Inter", 36 * -1)
        )

        canvas.create_text(
            22.0,
            76.0,
            anchor="nw",
            text="You have entered an incorrect username or password.",
            fill="#FFFFFF",
            font=("Inter", 17 * -1)
        )

        canvas.create_text(
            105.0,
            97.0,
            anchor="nw",
            text="Please check the fields and try again.",
            fill="#FFFFFF",
            font=("Inter", 17 * -1)
        )

        button_image_1 = PhotoImage(
            file=relative_to_assets("button_1.png"))
        button_1 = Button(
            image=button_image_1,
            borderwidth=0,
            highlightthickness=0,
            command=lambda: loginFailed(loginFailedWindow),
            relief="flat"
        )
        button_1.place(
            x=169.0,
            y=139.0,
            width=140.0,
            height=40.0
        )
        loginFailedWindow.resizable(False, False)
        loginFailedWindow.mainloop()

    def loginFailed(loginFailedWindow):
        loginFailedWindow.destroy()
        loginWindowFunction()
    def startOfFile():

        encryption_key = generate_or_load_key()
        cipher_suite = Fernet(encryption_key)

        retrieved_encrypted_data = get_encrypted_data()
        if retrieved_encrypted_data is not None:
            decrypted_data = decrypt_data(retrieved_encrypted_data, cipher_suite)
        atexit.register(lambda: os.remove(pidfile))

    icon_thread = threading.Thread(target=setup_tray_icon)
    icon_thread.start()
    startOfFile()


def is_admin():
    try:
        return ctypes.windll.shell32.IsUserAnAdmin()
    except:
        return False

if is_admin():
    main()
else:
    # Re-run the program with admin rights
    ctypes.windll.shell32.ShellExecuteW(None, "runas", sys.executable, " ".join(sys.argv), None, 1)
