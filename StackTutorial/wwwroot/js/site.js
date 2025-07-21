document.addEventListener('DOMContentLoaded', function () {
    // Constants
    const ROTATED_CLASS = 'rotated';
    const VISIBLE_CLASS = 'visible';
    const ACTIVE_CLASS = 'active';
    const ANIMATION_DELAY = 300;

    // Cache DOM elements
    const menuHeaders = document.querySelectorAll('.menu-header');
    const topicHeaders = document.querySelectorAll('.topic-header');
    const activeContent = document.querySelector(`.menu-content.${ACTIVE_CLASS}`);
    const sidebarToggler = document.querySelector('.navbar-toggler');
    const sidebar = document.querySelector('.side-menu');

    // Helper functions
    const toggleSection = (section, icon) => {
        if (!section || !icon) return;

        const isVisible = section.classList.contains(VISIBLE_CLASS);
        section.classList.toggle(VISIBLE_CLASS, !isVisible);
        icon.classList.toggle(ROTATED_CLASS, !isVisible);
    };

    const expandParentSections = (element, sectionSelector, headerSelector) => {
        const parentSection = element.closest(sectionSelector);
        if (!parentSection) return;

        const sectionId = parentSection.getAttribute('data-id');
        const section = document.getElementById(sectionId);
        const header = parentSection.querySelector(headerSelector);

        if (section && header) {
            section.classList.add(VISIBLE_CLASS);
            header.querySelector('.toggle-icon')?.classList.add(ROTATED_CLASS);
        }
    };

    // Event handlers
    const handleMenuHeaderClick = (e) => {
        const header = e.currentTarget;
        if (header.getAttribute('href')) return;

        e.preventDefault();
        const categoryId = header.getAttribute('data-category-id');
        const topicsSection = document.getElementById(`topics-${categoryId}`);
        const icon = header.querySelector('.toggle-icon');

        toggleSection(topicsSection, icon);
    };

    const handleTopicHeaderClick = (e) => {
        const header = e.currentTarget;
        if (header.getAttribute('href')) return;

        e.preventDefault();
        const topicId = header.parentElement.getAttribute('data-topic-id');
        const contentsSection = document.getElementById(`contents-${topicId}`);
        const icon = header.querySelector('.toggle-icon');

        toggleSection(contentsSection, icon);
    };

    const handleSidebarToggle = () => {
        sidebar?.classList.toggle('d-none');
    };

    // Initialize menu behavior
    const initializeMenu = () => {
        // Set initial state for all sections
        document.querySelectorAll('.menu-section').forEach(section => {
            section.classList.remove(VISIBLE_CLASS);
        });

        // Expand active content path
        if (activeContent) {
            // Expand topic and its contents
            expandParentSections(activeContent, '.menu-topic', '.topic-header');

            // Expand category and its topics
            expandParentSections(activeContent, '.menu-category', '.menu-header');

            // Scroll to active content
            setTimeout(() => {
                activeContent.scrollIntoView({
                    behavior: 'smooth',
                    block: 'nearest',
                    inline: 'start'
                });
            }, ANIMATION_DELAY);
        }
    };

    // Event listeners
    menuHeaders.forEach(header => {
        header.addEventListener('click', handleMenuHeaderClick);
    });

    topicHeaders.forEach(header => {
        header.addEventListener('click', handleTopicHeaderClick);
    });

    if (sidebarToggler) {
        sidebarToggler.addEventListener('click', handleSidebarToggle);
    }

    // Initialize
    initializeMenu();

    // Cleanup function (for single-page applications)
    return () => {
        menuHeaders.forEach(header => {
            header.removeEventListener('click', handleMenuHeaderClick);
        });
        topicHeaders.forEach(header => {
            header.removeEventListener('click', handleTopicHeaderClick);
        });
        if (sidebarToggler) {
            sidebarToggler.removeEventListener('click', handleSidebarToggle);
        }
    };
});

// Show loader when search form is submitted
$(document).on('submit', '.search-form', function () {
    $('.search-loader').addClass('active');
});

// Global function to hide loader
window.hideSearchLoader = function () {
    $('.search-loader').removeClass('active');
};

// Hide loader when page loads (in case of form submission)
$(document).ready(function () {
    hideSearchLoader();
});