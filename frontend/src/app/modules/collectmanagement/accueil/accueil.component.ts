import { AfterViewInit, Component, ElementRef, OnDestroy, Renderer2 } from '@angular/core';

@Component({
  selector: 'app-accueil',
  standalone: true,
  imports: [],
  templateUrl: './accueil.component.html',
  styleUrl: './accueil.component.scss'
})
export class AccueilComponent implements AfterViewInit, OnDestroy {
    private particlesArray: Particle[] = [];
    private numberOfParticles = 120;
    private ctx!: CanvasRenderingContext2D;
    private canvas!: HTMLCanvasElement;
    private mouseMoveListener!: () => void;

    constructor(private el: ElementRef, private renderer: Renderer2) {}

    ngAfterViewInit() {
        // Récupération des éléments
        this.canvas = this.el.nativeElement.querySelector("#particles");
        this.ctx = this.canvas.getContext("2d")!;
        this.resizeCanvas();

        // Initialisation des particules
        this.initParticles();
        this.animateParticles();
        this.drawConnections();

        // Ajout de l'écouteur de mouvement de la souris
        this.mouseMoveListener = this.renderer.listen('window', 'mousemove', (event: MouseEvent) => {
            this.handleMouseMove(event);
        });

        // Ajout de l'écouteur de redimensionnement de la fenêtre
        window.addEventListener("resize", () => this.resizeCanvas());
    }

    ngOnDestroy() {
        // Suppression des écouteurs d'événements pour éviter les fuites de mémoire
        if (this.mouseMoveListener) {
            this.mouseMoveListener();
        }
        window.removeEventListener("resize", () => this.resizeCanvas());
    }

    //  Redimensionner le canvas pour s'adapter à la fenêtre
    private resizeCanvas() {
        this.canvas.width = window.innerWidth;
        this.canvas.height = window.innerHeight;
    }

    //Initialiser les particules
    private initParticles() {
        this.particlesArray = [];
        for (let i = 0; i < this.numberOfParticles; i++) {
            this.particlesArray.push(new Particle(this.canvas));
        }
    }

    // Animation des particules
    private animateParticles() {
        this.ctx.clearRect(0, 0, this.canvas.width, this.canvas.height);
        this.particlesArray.forEach(particle => {
            particle.update();
            particle.draw(this.ctx);
        });
        requestAnimationFrame(() => this.animateParticles());
    }

    // Dessiner les connexions entre les particules
    private drawConnections() {
        this.ctx.strokeStyle = "#00e5ff";
        this.ctx.lineWidth = 0.5;

        for (let i = 0; i < this.particlesArray.length; i++) {
            for (let j = i + 1; j < this.particlesArray.length; j++) {
                const p1 = this.particlesArray[i];
                const p2 = this.particlesArray[j];
                const dist = Math.hypot(p2.x - p1.x, p2.y - p1.y);

                if (dist < 100) {
                    this.ctx.beginPath();
                    this.ctx.moveTo(p1.x, p1.y);
                    this.ctx.lineTo(p2.x, p2.y);
                    this.ctx.stroke();
                }
            }
        }
        requestAnimationFrame(() => this.drawConnections());
    }

    // Effet interactif avec la souris
    private handleMouseMove(event: MouseEvent) {
        const logo = this.el.nativeElement.querySelector(".logo-container");
        const moveX = (event.clientX - window.innerWidth / 2) * 0.02;
        const moveY = (event.clientY - window.innerHeight / 2) * 0.02;
        this.renderer.setStyle(logo, 'transform', `rotateY(${moveX}deg) rotateX(${moveY}deg)`);
    }
}

// Classe de particule
class Particle {
    x: number;
    y: number;
    size: number;
    speedX: number;
    speedY: number;
    canvas: HTMLCanvasElement;

    constructor(canvas: HTMLCanvasElement) {
        this.canvas = canvas;
        this.x = Math.random() * canvas.width;
        this.y = Math.random() * canvas.height;
        this.size = Math.random() * 3 + 1;
        this.speedX = Math.random() * 1.5 - 0.75;
        this.speedY = Math.random() * 1.5 - 0.75;
    }

    update() {
        this.x += this.speedX;
        this.y += this.speedY;
        if (this.x > this.canvas.width || this.x < 0) this.speedX *= -1;
        if (this.y > this.canvas.height || this.y < 0) this.speedY *= -1;
    }

    draw(ctx: CanvasRenderingContext2D) {
        ctx.fillStyle = "#00e5ff";
        ctx.beginPath();
        ctx.arc(this.x, this.y, this.size, 0, Math.PI * 2);
        ctx.closePath();
        ctx.fill();
    }
}
