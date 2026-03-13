export class inputManager {
    static numberStepUp(element) {
        element.stepUp();
        this.dispatchInput(element);
    }

    static numberStepDown(element) {
        element.stepDown();
        this.dispatchInput(element);
    }

    static dispatchInput(element) {
        element.dispatchEvent(new Event('input', {bubbles: true}))
    }
}