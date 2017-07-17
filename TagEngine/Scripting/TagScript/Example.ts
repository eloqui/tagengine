// Example TagScript file
/* Also comments
	like this should be fine. */
	
Room shuttle {
	title: "Shuttlecraft";
	description: "The small shuttlecraft";
	isAccessible: false;
	exits {
		west: mess;
	}
}

Room mess {
	title: "The Mess";
	description: "The eating lounge";
	isAccessible: true;
	exits {
		east: shuttle;
		west: observation;
	}
}

Room observation {
	title: "Observation Room";
	description: "The observation room";
	isAccessible: true;
	exits {
		east: mess;
	}
}

Item powerdevice {
	title: "Power Device";
	description: "A power source";
	
}

Npc captain {
	title: "Captain";
	description: "He's wearing his captain's uniform";
}