from __future__ import annotations

from typing import Tuple

import cv2
import numpy as np
from ultralytics import YOLO
import supervision as sv


class DoorCounter:
    def __init__(self, yolo_weights: str, door_line: Tuple[Tuple[int, int], Tuple[int, int]] = ((320, 100), (320, 380))):
        self.model = YOLO(yolo_weights)
        self.tracker = sv.ByteTrack(track_activation_threshold=0.25)
        self.line_zone = sv.LineZone(start=sv.Point(*door_line[0]), end=sv.Point(*door_line[1]))
        self.annotator = sv.LineZoneAnnotator()
        self.box_annotator = sv.BoxAnnotator()

    def process_frame(self, frame: np.ndarray):
        result = self.model.predict(frame, classes=[0], conf=0.25, verbose=False)[0]
        detections = sv.Detections.from_ultralytics(result)
        tracks = self.tracker.update_with_detections(detections)
        self.line_zone.trigger(tracks)

        annotated = self.box_annotator.annotate(frame.copy(), tracks)
        annotated = self.annotator.annotate(annotated, self.line_zone)
        return annotated, self.line_zone.in_count, self.line_zone.out_count
