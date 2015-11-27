﻿using System.Collections.Generic;
using MiniCraft.Gfx;

namespace MiniCraft.Entities
{
    public class Spark : Entity
    {
        private int lifeTime;
        public double xa, ya;
        public double xx, yy;
        private int time;
        private AirWizard owner;

        public Spark(AirWizard owner, double xa, double ya)
        {
            this.owner = owner;
            xx = this.x = owner.x;
            yy = this.y = owner.y;
            xr = 0;
            yr = 0;

            this.xa = xa;
            this.ya = ya;

            lifeTime = 60 * 10 + random.nextInt(30);
        }

        public override void tick() {
		time++;
		if (time >= lifeTime) {
			remove();
			return;
		}
		xx += xa;
		yy += ya;
		x = (int) xx;
		y = (int) yy;
		List<Entity> toHit = level.getEntities(x, y, x, y);
		for (int i = 0; i < toHit.size(); i++) {
			Entity e = toHit.get(i);
			if (e is Mob && !(e is AirWizard)) {
				e.hurt(owner, 1, ((Mob) e).dir ^ 1);
			}
		}
	}

        public override bool isBlockableBy(Mob mob)
        {
            return false;
        }

        public override void render(Screen screen)
        {
            if (time >= lifeTime - 6 * 20)
            {
                if (time / 6 % 2 == 0) return;
            }

            int xt = 8;
            int yt = 13;

            screen.render(x - 4, y - 4 - 2, xt + yt * 32, ColorHelper.get(-1, 555, 555, 555), random.nextInt(4));
            screen.render(x - 4, y - 4 + 2, xt + yt * 32, ColorHelper.get(-1, 000, 000, 000), random.nextInt(4));
        }
    }
}
