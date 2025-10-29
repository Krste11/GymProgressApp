const apiUrl = "https://localhost:7221/api/Exercise";

// Format date like "29/10/2025"
function formatDate(dateString)
{
    const date = new Date(dateString);
    return date.toLocaleDateString("en-GB"); // en-GB gives DD/MM/YYYY format
}

async function loadExercises()
{
    try
    {
        const response = await fetch(apiUrl);
        if (!response.ok) throw new Error("Failed to fetch data");

        const exercises = await response.json();
        const tableBody = document.querySelector("#exerciseTable tbody");
        tableBody.innerHTML = "";

        exercises.forEach(ex =>
        {
            const row = document.createElement("tr");

            // If your model doesn't yet have a Date property, we use created date now
            const date = ex.date ? formatDate(ex.date) : formatDate(new Date());

            row.innerHTML = `
        <td>${date}</td>
        <td>${ex.name}</td>
        <td>${ex.weight}</td>
        <td>${ex.repetitions}</td>
        <td><button class="delete-btn" data-id="${ex.id}">Delete</button></td>
      `;

            tableBody.appendChild(row);
        });

        // Attach delete listeners
        document.querySelectorAll(".delete-btn").forEach(button =>
        {
            button.addEventListener("click", async (e) =>
            {
                const id = e.target.getAttribute("data-id");
                await deleteExercise(id);
            });
        });

    } catch (error)
    {
        console.error("Error loading exercises:", error);
    }
}

async function addExercise(e)
{
    e.preventDefault();
    const name = document.getElementById("exerciseName").value.trim();
    const weight = parseFloat(document.getElementById("exerciseWeight").value);
    const reps = parseInt(document.getElementById("exerciseReps").value);
    const date = new Date().toISOString();

    if (!name || isNaN(weight) || isNaN(reps))
    {
        alert("Please fill all fields correctly!");
        return;
    }

    const exercise = { name, weight, repetitions: reps, date };
    const statusMsg = document.getElementById("statusMsg");

    try
    {
        const response = await fetch(apiUrl, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(exercise),
        });

        if (response.ok)
        {
            statusMsg.textContent = "✅ Exercise added successfully!";
            statusMsg.style.color = "green";
            document.getElementById("exerciseForm").reset();
            loadExercises();
        } else
        {
            throw new Error("Failed to add exercise");
        }
    } catch (error)
    {
        console.error(error);
        statusMsg.textContent = "❌ Error adding exercise.";
        statusMsg.style.color = "red";
    }
}

async function deleteExercise(id)
{
    if (!confirm("Are you sure you want to delete this exercise?")) return;

    try
    {
        const response = await fetch(`${apiUrl}/${id}`, { method: "DELETE" });
        if (response.ok)
        {
            loadExercises();
        } else
        {
            throw new Error("Failed to delete exercise");
        }
    } catch (error)
    {
        console.error(error);
        alert("Error deleting exercise.");
    }
}

// Load data when page opens
document.addEventListener("DOMContentLoaded", loadExercises);
document.getElementById("exerciseForm").addEventListener("submit", addExercise);