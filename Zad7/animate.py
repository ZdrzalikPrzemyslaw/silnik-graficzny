import argparse
import os
import string
import time

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
    # save_dir += '' if save_dir[-1] == '/' else '/'
    if not os.path.exists(save_dir):
        if not os.path.isdir(save_dir):
            os.mkdir(save_dir)

if args.fps is not None:
    if args.fps > 0:
        fps = args.fps
    else:
        raise ValueError('Value should be greater than 0.')

filelist=os.listdir(save_dir)
for fichier in filelist[:]: # filelist[:] makes a copy of filelist.
    if not(fichier.endswith(".png")):
        filelist.remove(fichier)

frames = []
for i in filelist:
    r: string = save_dir + '/' + str(i)
    new_frame = Image.open(r)
    frames.append(new_frame)

frames[0].save(save_dir + '/gif.gif', format='GIF',
               append_images=frames[1:],
               save_all=True,
               duration=len(filelist) / fps, loop=10)

for i in filelist:
    r: string = save_dir + '/' + str(i)
    os.remove(r)

