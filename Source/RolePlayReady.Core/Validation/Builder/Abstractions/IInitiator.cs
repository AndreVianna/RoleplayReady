namespace System.Validation.Builder.Abstractions;

public interface IInitiator<out TValidator> {
    TValidator Is();
}