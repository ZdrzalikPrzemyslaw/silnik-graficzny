import argparse
import os
import string

from PIL import Image

parser = argparse.ArgumentParser(description='Process path and FPS')
parser.add_argument(
    '-d',
    '--dir',
    metavar='DIR',
    type=str,
    action='store',
    dest='dir',
    help='Dir for reading files'
)
parser.add_argument(
    '-f',
    '--fps',
    metavar='NUM',
    type=int,
    dest='fps',
    action='store',
    help='Number of FPS'
)

args = parser.parse_args()
save_dir = ''
fps = 0
if args.dir is not None:
    save_dir = args.dir
    save_dir += '' if save_dir[-1] == '/' else '/'
    if not os.path.exists(save_dir):
        if not os.path.isdir(save_dir):
            os.mkdir(save_dir)

if args.fps is not None:
    if args.fps > 0:
        fps = args.fps
    else:
        raise ValueError('Value should be greater than 0.')

number_of_files = len([name for name in os.listdir(save_dir)])

frames = []
for i in range(1, number_of_files + 1):
    r: string = save_dir + str(i) + ".png"
    new_frame = Image.open(r)
    frames.append(new_frame)

frames[0].save('gif.gif', format='GIF',
               append_images=frames[1:],
               save_all=True,
               duration=number_of_files / fps, loop=10)
